using System;
using System.Threading.Tasks;
using AutoMapper;
using Chibbis.TestProject.Contracts.Product.Queries;
using Chibbis.TestProject.ProductManagementService.Application.Services;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Chibbis.TestProject.ProductManagementService.Application.Consumers.Product.Queries.GetProduct
{
    public class GetProductQueryConsumer: IConsumer<GetProductQuery>
    {
        private readonly IProductService _productService;
        private readonly ILogger<GetProductQueryConsumer> _logger;
        private readonly IMapper _mapper;

        public GetProductQueryConsumer(
            IProductService productService,
            ILogger<GetProductQueryConsumer> logger,
            IMapper mapper)
        {
            _productService = productService;
            _logger = logger;
            _mapper = mapper;
        }

        
        
        public async Task Consume(ConsumeContext<GetProductQuery> context)
        {
            var query = context.Message;
            GetProductQueryResponse response;
            try
            {
                var product = await _productService.GetProductAsync(query.ProducId);

                response = new GetProductQueryResponse() {Data = product};
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