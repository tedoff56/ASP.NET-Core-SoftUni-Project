using LightBulbsStore.Core.Models.Order;
using LightBulbsStore.Core.Services.Contracts;
using LightBulbsStore.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LightBulbsStore.Controllers
{
    public class OrderController : BaseController
    {
        private readonly UserManager<User> userManager;

        private readonly IOrderService orderService;

        private readonly ICartService cartService;

        private readonly IUserService userService;

        public OrderController(
            UserManager<User> _userManager,
            IOrderService _orderServic,
            ICartService _cartService,
            IUserService _userService)
        {
            userManager = _userManager;
            orderService = _orderServic;
            cartService = _cartService;
            userService = _userService;
        }

        private string UserId => userManager.GetUserId(User);

        public async Task<IActionResult> Details(OrderDetailsViewModel orderModel)
        {
            orderModel = await orderService.GetCartOrderDetailsAsync(UserId);

            return View(orderModel);
        }

        [HttpPost]
        public async Task<IActionResult> Place(OrderDetailsViewModel orderModel)
        {
            orderModel.Products = (await cartService.GetProductsAsync(UserId)).ToList();

            //if (!ModelState.IsValid)
            //{
            //    return View(nameof(Index));
            //}

            await userService.SetCustomerInfoAsync(UserId, orderModel.Customer);

            await orderService.CreateOrderAsync(orderModel, UserId);

            return Redirect($"/{nameof(Cart)}/");
        }



    }
}
