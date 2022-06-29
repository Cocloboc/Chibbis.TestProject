using System;
using System.Threading.Tasks;
using AutoMapper;
using Chibbis.TestProject.Contracts.Common;
using Chibbis.TestProject.ProductManagementService.Application.Services;
using MassTransit;
using Microsoft.Extensions.Logging;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace Chibbis.TestProject.ProductManagementService.Application.Consumers.Product.Commands.CreateProduct
{
    public class CreateProductCommandConsumer: IConsumer<CreateProductCommand>
    {
        private ILogger<CreateProductCommandConsumer> _logger;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public CreateProductCommandConsumer(
            ILogger<CreateProductCommandConsumer> logger,
            IProductService productService,
            IMapper mapper)
        {
            _logger = logger;
            _productService = productService;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<CreateProductCommand> context)
        {
            var command = context.Message;
            try
            {
                var product = _mapper.Map<Entities.Product>(command);
                await _productService.CreateProductAsync(product);
            }
            catch (Exception e)
            {
                _logger.LogError($"We have some errors:{e.Message}", e);
                throw;
            }

            await context.RespondAsync(new CreateProductCommandResponse());
        }
    }
}