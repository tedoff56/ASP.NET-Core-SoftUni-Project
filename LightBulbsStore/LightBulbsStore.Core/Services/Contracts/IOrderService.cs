using LightBulbsStore.Core.Models.Order;
using LightBulbsStore.Infrastructure.Data.Enumerations;

namespace LightBulbsStore.Core.Services.Contracts
{
    public interface IOrderService
    {
        Task<OrderDetailsViewModel> GetCartOrderDetailsAsync(string userId);

        Task<OrderDetailsViewModel> CreateOrderAsync(string userId);

        Task<List<OrderDetailsAdminViewModel>> GetAllOrdersAdminAsync();

        Task<OrderDetailsAdminViewModel> GetOrderDetailsAdminAsync(string orderId);

        Task<OrderDetailsViewModel> GetOrderDetailsCustomerAsync(string orderId);

        Task SetStatus(string orderId, OrderStatus status);

        Task<List<OrderProductViewModel>> GetOrderProductsAsync(string orderId);

        Task PlaceOrderAsync(OrderDetailsViewModel orderModel);
    }
}
