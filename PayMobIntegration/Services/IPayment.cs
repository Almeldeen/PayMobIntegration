using PayMobIntegration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayMobIntegration.Services
{
    public interface IPayment
    {
        public Task<string> GetToken();
        public Task<string> RegisterOrder(string Token, List<CartItem> Items);
        public Task<string> GetPaymentToken(string Token, string OrderId, decimal Total);
    }
}
