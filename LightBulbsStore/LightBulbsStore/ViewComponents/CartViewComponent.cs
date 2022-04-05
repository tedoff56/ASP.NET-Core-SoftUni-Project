using LightBulbsStore.Core.Models;
using LightBulbsStore.Core.Services.Contracts;
using LightBulbsStore.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LightBulbsStore.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        private readonly UserManager<User> userManager;

        private readonly ICartService cartService;

        public CartViewComponent(
            ICartService _cartService,
            UserManager<User> _userManager)
        {
            cartService = _cartService;
            userManager = _userManager;
        }

        public IViewComponentResult Invoke()
        {
            var userId = userManager.GetUserId(HttpContext.User);

            var model = new LayoutViewModel 
            { 
                CartProductsCount = cartService.TotalCartItems(userId).Result,
                CartTotalPrice = cartService.TotalPrice(userId).Result,
            };

            return View(model);
        }
    }
}
