using System.Collections.Generic;
using Chibbis.TestProject.CartService.Application.Consumers.Cart.Common;
using Chibbis.TestProject.Contracts.Common;

namespace Chibbis.TestProject.CartService.Application.Consumers.Cart.Commands.RemoveProducts
{
    public class RemoveProductsCommand
    {
        public List<ProductMinified> Products { get; set; }
    }

    public record RemoveProductsCommandResponse : CommandResponse
    {
        
    }
}