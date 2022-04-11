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
        Task<OrderViewModel> GetOrderDetailsAsync(string userId);

        Task CreateOrderAsync(OrderViewModel orderModel, string userId);
    }
}
