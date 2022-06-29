using System;
using System.Collections.Generic;
using Chibbis.TestProject.CartService.Application.Entities;

namespace Chibbis.TestProject.CartService.Application.Consumers.Cart.Common
{
    public class Cart
    {
        public Guid CartUUID { get; set; }
        public List<CartItem> CartItems { get; set; }
        public DateTime ExpireAt { get; set; }
        public string UserUUID { get; set; }
    }
}