using System;
using System.Threading.Tasks;
using AutoMapper;
using Chibbis.TestProject.ProductManagementService.Application.Consumers.Product.Commands.CreateProduct;
using Chibbis.TestProject.ProductManagementService.Application.Services;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Chibbis.TestProject.ProductManagementService.Application.Consumers.Product.Commands.UpdateProduct
{
    public class UpdateProductCommandConsumer : IConsumer<UpdateProductCommand>
    {
        private readonly ILogger<UpdateProductCommandConsumer> _logger;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public UpdateProductCommandConsumer(
            ILogger<UpdateProductCommandConsumer> logger,
            IProductService productService,
            IMapper mapper)
        {
            _logger = logger;
            _productService = productService;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<UpdateProductCommand> context)
        {
            var command = context.Message;
            try
            {
                var product = _mapper.Map<Entities.Product>(command);
                await _productService.UpdateProductAsync(product);
            }
            catch (Exception e)
            {
                _logger.LogError($"We have some errors:{e.Message}", e);
                throw;
            }

            await context.RespondAsync(new UpdateProductCommandResponse());
        }
    }
}