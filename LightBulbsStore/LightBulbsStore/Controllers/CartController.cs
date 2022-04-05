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

        public async Task<IActionResult> Index(CartViewModel cartViewModel)
        {
            var userId = (await userManager.GetUserAsync(User)).Id;

            var cartProducts = await cartService.GetProducts(userId);

            cartViewModel.Products = cartProducts;

            cartViewModel.TotalPrice = cartProducts.Sum(p => p.Quantity * p.Price);

            return View(cartViewModel);
        }

        public async Task<IActionResult> AddProduct(string productId)
        {
            var userId = (await userManager.GetUserAsync(User)).Id;

            bool productAdded = await cartService.AddProduct(productId, userId);

            return Redirect("/Product/");

        }

        public async Task<IActionResult> Update(CartViewModel cartViewModel)
        {
            await cartService.UpdateCart(cartViewModel);

            return View(cartViewModel);

        }

    }
}
