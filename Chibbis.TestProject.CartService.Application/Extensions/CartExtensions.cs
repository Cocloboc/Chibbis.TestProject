using System.Collections.Generic;
using System.Linq;
using Chibbis.TestProject.CartService.Application.Consumers.Cart.Common;
using Chibbis.TestProject.CartService.Application.Entities;
using Chibbis.TestProject.Contracts.Product.Common;

namespace Chibbis.TestProject.CartService.Application.Extensions
{
    public static class CartExtensions
    {
        public static CartMinified AddProducts(this CartMinified cartMinified, List<CartItemMinified> cartItems)
        {
            foreach (var cartItem in cartItems)
            {
                var oldCartItem = cartMinified
                    .CartItems
                    .FirstOrDefault(x => x.ProductId == cartItem.ProductId);

                if (oldCartItem == default)
                {
                    cartMinified.CartItems.Add(cartItem);
                }
                else
                {
                    oldCartItem.Amount += cartItem.Amount;
                }
            }

            return cartMinified;
        }
        
        public static CartMinified RemoveProducts(this CartMinified cartMinified, List<CartItemMinified> cartItems)
        {
            foreach (var cartItem in cartItems)
            {
                var oldCartItem = cartMinified
                    .CartItems
                    .FirstOrDefault(x => x.ProductId == cartItem.ProductId);

                if (oldCartItem == default)
                {
                    continue;
                }

                if (cartItem.Amount >= oldCartItem.Amount)
                {
                    cartMinified.CartItems.Remove(oldCartItem);
                }
                else
                {
                    oldCartItem.Amount -= cartItem.Amount;
                }
            }

            return cartMinified;
        }

        public static Cart GetCartDto(this CartMinified cartMinified, List<Product> products)
        {
            var cartDto = new Cart()
            {
                CartUUID = cartMinified.CartUUID,
                CartItems = new List<CartItem>(),
                UserUUID = cartMinified.UserUUID,
                ExpireAt = cartMinified.ExpireAt
            };

            foreach (var cartItem in cartMinified.CartItems)
            {
                var product = products.FirstOrDefault(x => x.Id == cartItem.ProductId);

                if (product == default)
                {
                    continue;
                }
                
                cartDto.CartItems.Add(new CartItem() {Product = product, Amount = cartItem.Amount});
            }

            return cartDto;
        }
    }
}