using LightBulbsStore.Core.Services.Contracts;
using LightBulbsStore.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LightBulbsStore.Infrastructure.Data.Enumerations;

namespace LightBulbsStore.Areas.Admin.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService orderService;

        private readonly UserManager<User> userManager;


        public OrderController(
            IOrderService _orderService,
            UserManager<User> _userManager)
        {
            orderService = _orderService;
            userManager = _userManager;

        }

        private string UserId => userManager.GetUserId(User);

        public async Task<IActionResult> AllOrders()
        {
            var orders = await orderService.GetAllOrdersAdminAsync();

            return View(orders);
        }

        public async Task<IActionResult> View(string id)
        {
            var order = await orderService.GetOrderDetailsAdminAsync(id);

            if(order is null)
            {
                return RedirectToAction(nameof(AllOrders));
            }

            return View(order);
        }

        public async Task<IActionResult> Cancel(string id)
        {
            await orderService.SetStatus(id, OrderStatus.Cancelled);

            return RedirectToAction(nameof(AllOrders));
        }

        public async Task<IActionResult> Ship(string id)
        {
            await orderService.SetStatus(id, OrderStatus.Shipped);

            return RedirectToAction(nameof(AllOrders));
        }


        public async Task<IActionResult> Finish(string id)
        {
            await orderService.SetStatus(id, OrderStatus.Delivered);

            return RedirectToAction(nameof(AllOrders));
        }
    }
}
