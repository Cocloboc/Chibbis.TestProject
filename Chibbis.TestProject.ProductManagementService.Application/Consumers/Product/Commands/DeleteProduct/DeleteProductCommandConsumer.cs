using System;
using System.Threading.Tasks;
using Chibbis.TestProject.ProductManagementService.Application.Services;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Chibbis.TestProject.ProductManagementService.Application.Consumers.Product.Commands.RemoveProduct
{
    public class DeleteProductCommandConsumer: IConsumer<DeleteProductCommand>
    {
        private readonly IProductService _productService;
        private readonly ILogger<DeleteProductCommandConsumer> _logger;

        public DeleteProductCommandConsumer(
            IProductService productService,
            ILogger<DeleteProductCommandConsumer> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<DeleteProductCommand> context)
        {
            var command = context.Message;
            try
            {
                await _productService.DeleteProductAsync(command.Id);
            }
            catch (Exception e)
            {
                _logger.LogError($"We have some errors:{e.Message}", e);
                throw;
            }

            await context.RespondAsync(new DeleteProductCommandResponse());
        }
    }
}