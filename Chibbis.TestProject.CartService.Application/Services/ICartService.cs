using System.Collections.Generic;
using System.Threading.Tasks;
using Chibbis.TestProject.CartService.Application.Consumers.Cart.Common;
using Chibbis.TestProject.CartService.Application.Entities;

namespace Chibbis.TestProject.CartService.Application.Services
{
    public interface ICartService
    {
        Task<CartMinified> GetCartMinifiedAsync(string userUUID);
        Task<Cart> GetCartAsync(string userUUID);
        Task<List<CartMinified>> GetAllCartsMinifiedAsync();
        Task<List<Cart>> GetAllCartsAsync();
        Task AddProductsAsync(string userUUID, List<CartItemMinified> items);
        Task RemoveProductsAsync(string userUUID, List<CartItemMinified> items);
    }
}