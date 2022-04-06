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
            var cartProducts = await cartService.GetProducts(UserId);

            var cartViewModel = new CartViewModel()
            {
                Id = await cartService.GetCartId(UserId),
                Products = cartProducts,
                TotalPrice = cartProducts.Sum(p => p.Quantity * p.Price)
            };

            return View(cartViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(CartViewModel cartViewModel)
        {

            await cartService.UpdateCart(cartViewModel, UserId);

            return RedirectToAction("Index");

        }

        public async Task<IActionResult> AddProduct(string productId)
        {

            await cartService.AddProduct(productId, UserId);

            return Redirect("/Product/");

        }

        public async Task<IActionResult> RemoveProduct(string productId)
        {
            await cartService.RemoveProduct(UserId, productId);

            return RedirectToAction("Index");

        }


    }
}
