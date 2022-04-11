using LightBulbsStore.Core.Models.Customer;
using LightBulbsStore.Core.Models.Order;
using LightBulbsStore.Core.Services.Contracts;
using LightBulbsStore.Infrastructure.Data.Enumerations;
using LightBulbsStore.Infrastructure.Data.Models;
using LightBulbsStore.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightBulbsStore.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly ICartService cartService;

        private readonly IBulbsStoreDbRepository repo;

        public OrderService(
            ICartService _cartService,
            IBulbsStoreDbRepository _repo)
        {
            cartService = _cartService;
            repo = _repo;
        }

        public async Task CreateOrderAsync(OrderViewModel orderModel, string userId)
        {
            var customer = await repo.All<Customer>()
                .FirstOrDefaultAsync(c => c.UserId == userId);

            var order = new Order()
            {
                TotalCost = orderModel.TotalCost,
                Status = OrderStatus.BeingProcessed,
                CustomerId = customer.Id
            };

            await repo.AddAsync(order);

            var orderProducts = new List<OrderProduct>();

            foreach (var productViewModel in orderModel.Products)
            {
                orderProducts.Add(new OrderProduct()
                { 
                    OrderId = order.Id,
                    ProductId = productViewModel.Id,
                    Quantity = productViewModel.Quantity
                });
            }

            await cartService.EmptyCartAsync(userId);

            await repo.AddRangeAsync(orderProducts);

            await repo.SaveChangesAsync();

        }

        public async Task<OrderViewModel> GetOrderDetailsAsync(string userId)
        {
            var cartProducts = (await cartService.GetProductsAsync(userId)).ToList();

            var customer = await repo.All<Customer>()
                .Where(c => c.UserId == userId)
                .FirstOrDefaultAsync();

            var order = new OrderViewModel()
            {
                Products = cartProducts,
                Customer = new CustomerInfoViewModel()
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    PhoneNumber = customer.PhoneNumber,
                    City = customer.City,
                    Address = customer.Address,
                    ZipCode = customer.ZipCode
                }
            };

            return order;
        }
    }
}
