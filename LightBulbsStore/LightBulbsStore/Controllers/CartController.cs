using Humanizer;
using LightBulbsStore.Core.Models.Cart;
using LightBulbsStore.Core.Models.Product;
using LightBulbsStore.Core.Services.Contracts;
using LightBulbsStore.Core.Services.Models.Cart;
using LightBulbsStore.Infrastructure.Data.Models;
using LightBulbsStore.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Security.Claims;

namespace LightBulbsStore.Controllers
{
    public class CartController : BaseController
    {
        private readonly UserManager<User> userManager;

        private readonly ICartService cartService;

        public CartController(
            UserManager<User> _userManager,
            ICartService _cartService)
        {
            userManager = _userManager;
            cartService = _cartService;
        }

        private string UserId => userManager.GetUserId(User);

        public async Task<IActionResult> Index()
        {
            var cartModel = await cartService.GetCartModelAsync(UserId);

            return View(cartModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductViewModel model, [FromForm] string productId)
        {

            await cartService.AddProductAsync(new AddProductServiceModel()
            {
                ProductId = model.ProductId is null ? productId : model.ProductId,
                UserId = this.UserId,
                Quantity= model.QuantityToAdd < 1 ? 1 : model.QuantityToAdd,
            });

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RemoveProduct(string productId)
        {
            await cartService.RemoveProductAsync(UserId, productId);

            return RedirectToAction("Index");

        }

        [HttpPost]
        public async Task<IActionResult> Update([FromForm] UpdateProductQuantityServiceModel model)
        {
            model.UserId = this.UserId;

            await cartService.UpdateAsync(model);

            return RedirectToAction(nameof(Index));
        }


    }
}
