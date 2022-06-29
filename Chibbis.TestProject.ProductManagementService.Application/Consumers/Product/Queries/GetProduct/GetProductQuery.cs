using Chibbis.TestProject.Contracts.Common;

namespace Chibbis.TestProject.ProductManagementService.Application.Consumers.Product.Queries.GetProduct
{
    public record GetProductQuery
    {
        public int ProducId { get; init; }
    }

    public record GetProductQueryResponse : QueryResponse<Entities.Product>
    {
        
    }
}