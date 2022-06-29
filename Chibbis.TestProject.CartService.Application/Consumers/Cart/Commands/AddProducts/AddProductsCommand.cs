using System.Collections.Generic;
using Chibbis.TestProject.CartService.Application.Consumers.Cart.Common;
using Chibbis.TestProject.Contracts.Common;

namespace Chibbis.TestProject.CartService.Application.Consumers.Cart.Commands.AddProducts
{
    public record AddProductsCommand
    {
        public List<ProductMinified> Products { get; set; }
    }

    public record AddProductsCommandResponse : CommandResponse
    {
        
    }
}