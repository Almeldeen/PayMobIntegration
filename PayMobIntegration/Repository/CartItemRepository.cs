using PayMobIntegration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayMobIntegration.Repository
{
    public class CartItemRepository : ICartItemRepository
    {
        static List<CartItem> Items =  new List<CartItem>() {
                new CartItem() {
                    Id = Guid.NewGuid(),
                    Price = 350,
                    Product = "Consultation",
                    Quantity = 1
                }
        };

        public List<CartItem> CartItems()
        {
            return Items;
        }
    }
}
