using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chibbis.TestProject.Contracts.Product.Queries;
using Chibbis.TestProject.ProductManagementService.Application.Consumers.Product.Commands.CreateProduct;
using Chibbis.TestProject.ProductManagementService.Application.Services;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Chibbis.TestProject.ProductManagementService.Application.Consumers.Product.Queries.ValidateProducts
{
    public class ValidateProductsQueryConsumer: IConsumer<ValidateProductsQuery>
    {
        private readonly IProductService _productService;
        private readonly ILogger<ValidateProductsQuery> _logger;

        public ValidateProductsQueryConsumer(
            IProductService productService,
            ILogger<ValidateProductsQuery> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        
        
        public async Task Consume(ConsumeContext<ValidateProductsQuery> context)
        {
            var query = context.Message;
            ValidateProductsQueryResponse response;
            try
            {
                var invalidIds = await _productService.ValidateProducts(query.ProductsIds);

                response = new ValidateProductsQueryResponse() {Data = invalidIds};
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