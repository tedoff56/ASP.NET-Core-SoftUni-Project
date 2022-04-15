using LightBulbsStore.Core.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightBulbsStore.Core.Services.Contracts
{
    public interface IOrderService
    {
        Task<OrderDetailsViewModel> GetCartOrderDetailsAsync(string userId);

        Task CreateOrderAsync(OrderDetailsViewModel orderModel, string userId);

        Task<List<OrderAdminViewModel>> GetAllOrdersAsync();

        Task<OrderAdminViewModel> GetOrderDetailsAsync(string orderId);

        Task Delete(string orderId);

        Task Process(string orderId);
    }
}
