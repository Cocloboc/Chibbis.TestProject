using System;
using System.Threading.Tasks;
using Chibbis.TestProject.CartService.Application.Consumers.Cart.Commands.AddProducts;
using Chibbis.TestProject.CartService.Application.Consumers.Cart.Commands.RemoveProducts;
using Chibbis.TestProject.CartService.Application.Consumers.Cart.Common;
using Chibbis.TestProject.CartService.Application.Consumers.Cart.Queries.GetCart;
using Chibbis.TestProject.CartService.Web.Attributes;
using Chibbis.TestProject.Contracts.Product.Common;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace Chibbis.TestProject.CartService.Web.Controllers
{
    [UserData]
    [ApiController]
    [Route("[controller]")]
    public class CartController : Controller
    {
        private readonly IMediator _mediator;

        public CartController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost("Products")]
        public async Task<IActionResult> AddProducts(AddProductsCommand command)
        {
            await _mediator
                .CreateRequestClient<AddProductsCommand>()
                .GetResponse<AddProductsCommandResponse>(command);

            return Ok();
        }
        
        [HttpDelete("Products")]
        public async Task<IActionResult> RemoveProducts(RemoveProductsCommand command)
        {
            await _mediator
                .CreateRequestClient<RemoveProductsCommand>()
                .GetResponse<RemoveProductsCommandResponse>(command);

            return Ok();
        }
        
        [HttpGet]
        public async Task<Cart> GetCart()
        {
            var query = new GetCartQuery();
            
            var response = await _mediator
                .CreateRequestClient<GetCartQuery>()
                .GetResponse<GetCartQueryResponse>(query);

            return response.Message.Data;
        }
    }
}