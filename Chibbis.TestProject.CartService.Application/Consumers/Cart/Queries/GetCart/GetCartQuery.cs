using Chibbis.TestProject.CartService.Application.Consumers.Cart.Common;
using Chibbis.TestProject.Contracts.Common;

namespace Chibbis.TestProject.CartService.Application.Consumers.Cart.Queries.GetCart
{
    public record GetCartQuery
    {
        
    }
    
    public record GetCartQueryResponse: QueryResponse<Common.Cart>
    {
        
    }
}