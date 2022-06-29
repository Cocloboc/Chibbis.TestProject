using System;
using System.Collections.Generic;

namespace Chibbis.TestProject.CartService.Application.Entities
{
    public class CartMinified
    {
        public Guid CartUUID { get; } = Guid.NewGuid();
        public List<CartItemMinified> CartItems { get; set; }
        public string UserUUID { get; set; }
        public DateTime ExpireAt { get; set; }
    }
}