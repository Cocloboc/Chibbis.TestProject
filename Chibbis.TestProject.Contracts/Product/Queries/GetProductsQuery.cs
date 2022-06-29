using System.Collections.Generic;
using Chibbis.TestProject.Contracts.Common;
using Chibbis.TestProject.Contracts.Product.Common;

namespace Chibbis.TestProject.Contracts.Product.Queries
{
    public record GetProductsQuery
    {
        public List<int> ProductIds { get; init; }
    }

    public record GetProductsQueryResponse : QueryResponse<List<ProductDto>>
    {
        
    }
}