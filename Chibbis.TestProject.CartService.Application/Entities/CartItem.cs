using Chibbis.TestProject.CartService.Application.Entities;
using Chibbis.TestProject.Contracts.Product.Common;

namespace Chibbis.TestProject.CartService.Application.Consumers.Cart.Common
{
    public class CartItem
    {
        public Product Product { get; set; }
        public int Amount { get; set; }
    }
}