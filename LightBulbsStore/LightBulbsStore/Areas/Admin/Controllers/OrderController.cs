using LightBulbsStore.Core.Models.Order;
using LightBulbsStore.Core.Services.Contracts;
using LightBulbsStore.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
            var orders = await orderService.GetAllOrdersAsync();

            return View(orders);
        }

        public async Task<IActionResult> View(string id)
        {
            var order = await orderService.GetOrderDetailsAsync(id);

            return View(order);
        }

        public async Task<IActionResult> Delete(string id)
        {
            await orderService.Delete(id);

            return RedirectToAction(nameof(AllOrders));
        }

        public async Task<IActionResult> Process(string id)
        {
            await orderService.Process(id);

            return RedirectToAction(nameof(AllOrders));
        }
    }
}
