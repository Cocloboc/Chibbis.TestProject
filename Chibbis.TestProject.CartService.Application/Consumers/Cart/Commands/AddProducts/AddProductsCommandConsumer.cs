using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Chibbis.TestProject.CartService.Application.Consumers.Cart.Common;
using Chibbis.TestProject.CartService.Application.Entities;
using Chibbis.TestProject.CartService.Application.Exceptions;
using Chibbis.TestProject.CartService.Application.Services;
using Chibbis.TestProject.Contracts.Product.Queries;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Chibbis.TestProject.CartService.Application.Consumers.Cart.Commands.AddProducts
{
    public class AddProductsCommandConsumer : IConsumer<AddProductsCommand>
    {
        private readonly IUserContext _userContext;
        private readonly ILogger<AddProductsCommandConsumer> _logger;
        private readonly ICartService _cartService;
        private readonly IBus _bus;
        private readonly IMapper _mapper;

        public AddProductsCommandConsumer(
            ILogger<AddProductsCommandConsumer> logger,
            ICartService cartService,
            IBus bus,
            IUserContext userContext,
            IMapper mapper)
        {
            _logger = logger;
            _cartService = cartService;
            _bus = bus;
            _userContext = userContext;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<AddProductsCommand> context)
        {
            var command = context.Message;
            try
            {
                var userUUID = _userContext.UserUUID;
                var products = _mapper.Map<List<CartItemMinified>>(command.Products);
                
                await _cartService.AddProductsAsync(userUUID, products);
            }
            catch (Exception e)
            {
                _logger.LogError($"We have some errors:{e.Message}", e);
                throw;
            }

            await context.RespondAsync(new AddProductsCommandResponse());
        }
    }
}