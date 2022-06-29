using System.Collections.Generic;
using Chibbis.TestProject.Contracts.Common;

namespace Chibbis.TestProject.Contracts.Product.Queries
{
    public record ValidateProductsQuery
    {
        public List<int> ProductsIds { get; init; }
    }

    public record ValidateProductsQueryResponse : QueryResponse<List<int>>
    {
        
    }
}