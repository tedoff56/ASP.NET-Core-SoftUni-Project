using LightBulbsStore.Core.Models;
using LightBulbsStore.Core.Services.Contracts;
using LightBulbsStore.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LightBulbsStore.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        private readonly ICartService cartService;

        public CartViewComponent(
            ICartService _cartService,
            IHttpContextAccessor _httpContextAccessor)
        {
            cartService = _cartService;
            httpContextAccessor = _httpContextAccessor;
        }

        public IViewComponentResult Invoke()
        {
            var userId = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var model = new LayoutViewModel 
            { 
                CartProductsCount = cartService.TotalCartItems(userId).Result,
                CartTotalPrice = cartService.TotalPrice(userId).Result,
            };

            return View(model);
        }
    }
}
