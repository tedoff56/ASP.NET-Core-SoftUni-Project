using LightBulbsStore.Core.Models.Cart;
using LightBulbsStore.Core.Services.Contracts;
using LightBulbsStore.Infrastructure.Data.Models;
using LightBulbsStore.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
            ViewBag.CartIsEmpty = await cartService.IsEmpty(UserId);

            var cartProducts = await cartService.GetProductsAsync(UserId);

            var cartViewModel = new CartViewModel()
            {
                CartId = await cartService.GetCartIdAsync(UserId),
                Products = cartProducts,
                TotalPrice = cartProducts.Sum(p => p.Quantity * p.Price)
            };

            return View(cartViewModel);
        }

        public async Task<IActionResult> AddProduct(string productId)
        {

            await cartService.AddProductAsync(productId, UserId);

            return RedirectToAction(nameof(Index), 
                nameof(CartController).Replace("Controller", string.Empty), 
                cartService.GetCartIdAsync(UserId));

        }

        public async Task<IActionResult> RemoveProduct(string productId)
        {
            await cartService.RemoveProductAsync(UserId, productId);

            return RedirectToAction("Index");

        }

        [HttpPost]
        public async Task<IActionResult> Update(CartViewModel model)
        {
            await cartService.UpdateAsync(model);

            return RedirectToAction(nameof(Index));
        }


    }
}
