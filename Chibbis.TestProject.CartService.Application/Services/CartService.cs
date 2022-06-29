using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Chibbis.TestProject.CartService.Application.Consumers.Cart.Common;
using Chibbis.TestProject.CartService.Application.Entities;
using Chibbis.TestProject.CartService.Application.Exceptions;
using Chibbis.TestProject.CartService.Application.Extensions;
using Chibbis.TestProject.CartService.Application.Options;
using Chibbis.TestProject.Contracts.Product.Queries;
using MassTransit;
using MassTransit.Initializers;
using Microsoft.Extensions.Options;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace Chibbis.TestProject.CartService.Application.Services
{
    public class CartService : ICartService
    {
        private readonly string _prefix = "cart";
        private readonly IRedisClient _redisClient;
        private readonly IBus _bus;
        private readonly CartOptions _cartOptions;
        private readonly IMapper _mapper;

        public CartService(
            IRedisClient redisClient,
            IBus bus,
            IOptions<CartOptions> cartOptions,
            IMapper mapper)
        {
            _redisClient = redisClient;
            _bus = bus;
            _mapper = mapper;
            _cartOptions = cartOptions.Value;
        }

        public async Task<CartMinified> GetCartMinifiedAsync(string userUUID)
        {
            return await _redisClient
                .GetDefaultDatabase()
                .GetAsync<CartMinified>($@"{_prefix}:{userUUID}");
        }

        public async Task<Cart> GetCartAsync(string userUUID)
        {
            var cart = await _redisClient
                .GetDefaultDatabase()
                .GetAsync<CartMinified>($@"{_prefix}:{userUUID}");

            if (cart == default)
            {
                return default;
            }

            var productIds = cart.CartItems.Select(x => x.ProductId)
                .Distinct().ToList();

            var response = await _bus.CreateRequestClient<GetProductsQuery>()
                .GetResponse<GetProductsQueryResponse>(new GetProductsQuery() {ProductIds = productIds});

            var productsDto = response.Message.Data;
            var products = _mapper.Map<List<Product>>(productsDto);

            return cart.GetCartDto(products);
        }

        public async Task<List<Cart>> GetAllCartsAsync()
        {
            var cartsKeys = await _redisClient.GetDefaultDatabase()
                .SearchKeysAsync($"{_prefix}:*");

            var carts = (await _redisClient.GetDefaultDatabase()
                .GetAllAsync<CartMinified>(cartsKeys.ToArray())).Values;

            var productIds = carts.SelectMany(x => x.CartItems).Select(x => x.ProductId)
                .Distinct().ToList();

            var response = await _bus.CreateRequestClient<GetProductsQuery>()
                .GetResponse<GetProductsQueryResponse>(new GetProductsQuery() {ProductIds = productIds});
            var productsDto = response.Message.Data;
            var products = _mapper.Map<List<Product>>(productsDto);

            return carts.Select(x => x.GetCartDto(products)).ToList();
        }

        public async Task<List<CartMinified>> GetAllCartsMinifiedAsync()
        {
            var cartsKeys = await _redisClient.GetDefaultDatabase()
                .SearchKeysAsync($"{_prefix}:*");

            var carts = await _redisClient.GetDefaultDatabase()
                .GetAllAsync<CartMinified>(cartsKeys.ToArray());

            return carts.Values.ToList();
        }

        public async Task AddProductsAsync(string userUUID, List<CartItemMinified> items)
        {
            var productIds = items.Select(x => x.ProductId).ToList();
            await ValidateProducts(productIds);
            
            var cart = await GetCartMinifiedAsync(userUUID);

            if (cart == default)
            {
                cart = new CartMinified()
                {
                    CartItems = items
                };
            }
            else
            {
                cart.AddProducts(items);
            }

            await SaveCartAsync(userUUID, cart);
        }
        
        public async Task RemoveProductsAsync(string userUUID, List<CartItemMinified> items)
        {
            var cart = await GetCartMinifiedAsync(userUUID);

            if (cart == default)
            {
                return;
            }
            else
            {
                cart.RemoveProducts(items);
            }

            await SaveCartAsync(userUUID, cart);
        }

        private async Task SaveCartAsync(string userUUID, CartMinified cartMinified)
        {
            cartMinified.UserUUID = userUUID;
            cartMinified.ExpireAt = DateTime.Now.AddDays(_cartOptions.LifeTimeInDays);

            await _redisClient.GetDefaultDatabase()
                .AddAsync($@"{_prefix}:{userUUID}", cartMinified, DateTimeOffset.Now.AddDays(_cartOptions.LifeTimeInDays));
        }
        
        private async Task ValidateProducts(List<int> productIds)
        {

            var response = await  _bus.CreateRequestClient<ValidateProductsQuery>()
                .GetResponse<ValidateProductsQueryResponse>(new ValidateProductsQuery
                {
                    ProductsIds = productIds
                });

            var responseMsg = response.Message;

            var invalidIds = responseMsg.Data;
            if (invalidIds.Any())
            {
                throw new NotFoundException($"The products with id:{string.Join(", ", invalidIds)} not found");
            }
        }
    }
}