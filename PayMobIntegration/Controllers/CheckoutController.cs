using Microsoft.AspNetCore.Mvc;
using PayMobIntegration.Services;
using PayMobIntegration.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PayMobIntegration.Models;

namespace PayMobIntegration.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly IPayment _PaymentSrvc;
        private readonly ICartItemRepository _CartRepo;

        public CheckoutController(IPayment PaymentSrvc, ICartItemRepository CartRepo)
        {
            _PaymentSrvc = PaymentSrvc;
            _CartRepo = CartRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Checkout()
        {
            var Token = await _PaymentSrvc.GetToken();
            var OrderId = await _PaymentSrvc.RegisterOrder(Token, _CartRepo.CartItems());
            var IframToken = await _PaymentSrvc.GetPaymentToken(Token, OrderId, _CartRepo.CartItems().Sum(x => x.Price * x.Quantity));
            return Redirect("https://accept.paymob.com/api/acceptance/iframes/737064?payment_token=" + IframToken);
        }

        public IActionResult PayMobCallBack([FromQuery] bool Success) => Success ? View("Success") : View("Failed");
    }
}
