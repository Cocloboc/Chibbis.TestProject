using System.Threading;
using System.Threading.Tasks;
using Chibbis.TestProject.ProductManagementService.Application.Consumers.Product.Commands.CreateProduct;
using Chibbis.TestProject.ProductManagementService.Application.Consumers.Product.Commands.RemoveProduct;
using Chibbis.TestProject.ProductManagementService.Application.Consumers.Product.Commands.UpdateProduct;
using Chibbis.TestProject.ProductManagementService.Application.Consumers.Product.Queries.GetProduct;
using Chibbis.TestProject.ProductManagementService.Application.Entities;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Chibbis.TestProject.ProductManagementService.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : Controller
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ProductMinified> CreateProduct(CreateProductCommand command, CancellationToken token)
        {
            var response = await _mediator
                .CreateRequestClient<CreateProductCommand>()
                .GetResponse<CreateProductCommandResponse>(command, token);

            return response.Message.Data;
        }
        
        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductCommand command, CancellationToken token)
        {
            await _mediator
                .CreateRequestClient<UpdateProductCommand>()
                .GetResponse<UpdateProductCommandResponse>(command, token);

            return Ok();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id,CancellationToken token)
        {
            var command = new DeleteProductCommand() {Id = id};
            
            await _mediator
                .CreateRequestClient<DeleteProductCommand>()
                .GetResponse<DeleteProductCommandResponse>(command, token);

            return Ok();
        }
        
        [HttpGet("{id}")]
        public async Task<Product> GetProduct(int id,CancellationToken token)
        {
            var query = new GetProductQuery() {ProducId = id};
            
            var response = await _mediator
                .CreateRequestClient<GetProductQuery>()
                .GetResponse<GetProductQueryResponse>(query, token);

            return response.Message.Data;
        }
    }
}