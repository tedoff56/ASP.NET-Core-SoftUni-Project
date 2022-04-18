using LightBulbsStore.Core.Models.Cart;
using LightBulbsStore.Core.Models.Product;
using LightBulbsStore.Core.Services.Contracts;
using LightBulbsStore.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LightBulbsStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;

        private readonly ICartService cartService;

        private readonly UserManager<User> userManager;


        public ProductController(
            IProductService _productService,
            ICartService _cartService,
            UserManager<User> _userManager)
        {
            productService = _productService;
            cartService = _cartService;
            userManager = _userManager;
        }

        private string UserId => userManager.GetUserId(User);

        public async Task<IActionResult> Index(int categoryId)
        {
            var result = await productService.GetAllProductsAsync(categoryId);

            return View(result);
        }

        public async Task<IActionResult> Info(string productId)
        {
            var product = await productService.GetProductAsync(productId);

            if (product is null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }
    }
}
