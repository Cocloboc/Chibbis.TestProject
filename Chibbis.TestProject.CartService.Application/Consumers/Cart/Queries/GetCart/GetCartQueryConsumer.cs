using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chibbis.TestProject.CartService.Application.Consumers.Cart.Common;
using Chibbis.TestProject.CartService.Application.Services;
using Chibbis.TestProject.Contracts.Product.Common;
using Chibbis.TestProject.Contracts.Product.Queries;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Chibbis.TestProject.CartService.Application.Consumers.Cart.Queries.GetCart
{
    public class GetCartQueryConsumer: IConsumer<GetCartQuery>
    {
        private readonly ICartService _cartService;
        private readonly ILogger<GetCartQueryConsumer> _logger;
        private readonly IUserContext _userContext;

        public GetCartQueryConsumer(
            ICartService cartService,
            ILogger<GetCartQueryConsumer> logger,
            IUserContext userContext)
        {
            _cartService = cartService;
            _logger = logger;
            _userContext = userContext;
        }

        
        
        public async Task Consume(ConsumeContext<GetCartQuery> context)
        {
            var query = context.Message;
            GetCartQueryResponse response;
            try
            {
                var cart = await _cartService.GetCartAsync(_userContext.UserUUID) ?? new Common.Cart()
                {
                    CartUUID = Guid.NewGuid(),
                    CartItems = new List<CartItem>()
                };

                response = new GetCartQueryResponse {Data = cart};
            }
            catch (Exception e)
            {
                _logger.LogError($"We have some errors:{e.Message}", e);
                throw;
            }

            await context.RespondAsync(response);
        }
    }
}