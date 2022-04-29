using LightBulbsStore.Core.Models.Cart;
using LightBulbsStore.Core.Models.Order;
using LightBulbsStore.Core.Services.Contracts;
using LightBulbsStore.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto;

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

        [HttpPost]
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

        public async Task<IActionResult> All()
        {
            var user = await userManager.FindByIdAsync(UserId);

            var result = (await orderService.GetAllOrdersAdminAsync())
                .Where(o => user.Customer.Orders.Any(co => co.Id == o.OrderId))
                .Select(o => new OrderDetailsViewModel()
                {
                    OrderId = o.OrderId,
                    OrderDate = o.OrderDate,
                    Status = o.OrderStatus,
                    TotalCost = o.OrderCost
                })
                .ToList();


            return View(result);
        }

        public async Task<IActionResult> View(string id)
        {
            var result = await orderService.GetOrderDetailsCustomerAsync(id);

            if(result is null)
            {
                return RedirectToAction(nameof(All));
            }


            return View(result);
        }



    }
}
