using LightBulbsStore.Core.Models.Cart;
using LightBulbsStore.Core.Models.Order;
using LightBulbsStore.Core.Services.Contracts;
using LightBulbsStore.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LightBulbsStore.Controllers
{
    public class OrderController : BaseController
    {
        private readonly List<OrderProductViewModel> orderProducts;

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

        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Details()
        {
            var orderModel = await orderService.CreateOrderAsync(UserId);

            return View(orderModel);
        }

        public async Task<IActionResult> Place(OrderDetailsViewModel orderModel)
        {
            orderModel.Products = await orderService.GetOrderProductsAsync(orderModel.OrderId);
            ModelState.Remove("Products");

            if (!ModelState.IsValid)
            {
                return View(nameof(Details), orderModel);
            }

            await orderService.PlaceOrderAsync(orderModel);

            

            return View();
        }



    }
}
