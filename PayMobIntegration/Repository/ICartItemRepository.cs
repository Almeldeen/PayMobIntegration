using PayMobIntegration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayMobIntegration.Repository
{
    public interface ICartItemRepository
    {
        public List<CartItem> CartItems();
    }
}
