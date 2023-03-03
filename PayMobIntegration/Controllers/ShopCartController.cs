using Microsoft.AspNetCore.Mvc;
using PayMobIntegration.Models;
using PayMobIntegration.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayMobIntegration.Controllers
{
    public class ShopCartController : Controller
    {
        private readonly ICartItemRepository _CartRepo;

        public ShopCartController(ICartItemRepository CartRepo)
        {
            _CartRepo = CartRepo;
        }

        public IActionResult Index()
        {
            return View(_CartRepo.CartItems());
        }
    }
}
