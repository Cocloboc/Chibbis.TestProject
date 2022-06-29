using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Chibbis.TestProject.Contracts.Product.Common;
using Chibbis.TestProject.Contracts.Product.Queries;
using Chibbis.TestProject.ProductManagementService.Application.Services;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Chibbis.TestProject.ProductManagementService.Application.Consumers.Product.Queries.GetProducts
{
    public class GetProductsQueryConsumer: IConsumer<GetProductsQuery>
    {
        private readonly IProductService _productService;
        private readonly ILogger<GetProductsQuery> _logger;
        private readonly IMapper _mapper;

        public GetProductsQueryConsumer(
            IProductService productService,
            ILogger<GetProductsQuery> logger,
            IMapper mapper)
        {
            _productService = productService;
            _logger = logger;
            _mapper = mapper;
        }

        
        
        public async Task Consume(ConsumeContext<GetProductsQuery> context)
        {
            var query = context.Message;
            GetProductsQueryResponse response;
            try
            {
                var products = await _productService.GetProductsAsync(query.ProductIds);
                var productsDto = _mapper.Map<List<ProductDto>>(products);
                
                response = new GetProductsQueryResponse() {Data = productsDto};
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