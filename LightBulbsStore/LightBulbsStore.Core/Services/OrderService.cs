using LightBulbsStore.Core.Models.Cart;
using LightBulbsStore.Core.Models.Customer;
using LightBulbsStore.Core.Models.Order;
using LightBulbsStore.Core.Models.Product;
using LightBulbsStore.Core.Services.Contracts;
using LightBulbsStore.Infrastructure.Data.Enumerations;
using LightBulbsStore.Infrastructure.Data.Models;
using LightBulbsStore.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightBulbsStore.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly ICartService cartService;

        private readonly IBulbsStoreDbRepository repo;

        private readonly IUserService userService;

        public OrderService(
            ICartService _cartService,
            IBulbsStoreDbRepository _repo,
            IUserService _userService)
        {
            cartService = _cartService;
            repo = _repo;
            userService = _userService;
        }

        public async Task CreateOrderAsync(OrderDetailsViewModel orderModel, string userId)
        {
            var customer = await repo.All<Customer>()
                .FirstOrDefaultAsync(c => c.UserId == userId);

            var order = new Order()
            {
                TotalCost = orderModel.Products is null ? 0 : orderModel.Products.Sum(p => p.Quantity * p.Price),
                Status = OrderStatus.BeingProcessed,
                CustomerId = customer.Id,
                OrderDate = DateTime.UtcNow
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

        public async Task<OrderDetailsViewModel> GetCartOrderDetailsAsync(string userId)
        {
            var cartProducts = (await cartService.GetProductsAsync(userId)).ToList();

            var customer = await repo.All<Customer>()
                .Where(c => c.UserId == userId)
                .FirstOrDefaultAsync();

            var orderDetails = new OrderDetailsViewModel()
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
                },
                TotalCost = cartProducts.Sum(cp => cp.Quantity * cp.Price)
            };

            return orderDetails;
        }


        public async Task<List<OrderAdminViewModel>> GetAllOrdersAsync()
        {
            var orders = await repo.All<Order>()
                .Where(o => o.Status == OrderStatus.BeingProcessed)
                .OrderByDescending(o => o.OrderDate)
                .Select(o => new OrderAdminViewModel
                {
                    UserEmail = o.Customer.User.UserName,
                    OrderId = o.Id,
                    OrderDate = o.OrderDate.ToString("f"),
                    TotalOrders = repo.All<Order>().Count(),
                    OrderCost = o.TotalCost,
                })
                .ToListAsync();



            return orders;
        }

        public async Task<OrderAdminViewModel> GetOrderDetailsAsync(string orderId)
        {
            var products = await repo.All<OrderProduct>()
                .Where(op => op.OrderId == orderId)
                .Select(op => new { op.Product, op.Quantity })
                .ToListAsync();

            var order = await repo.All<Order>()
                .Select(o => new OrderAdminViewModel
                {
                    Customer = new CustomerInfoViewModel()
                    {
                        FirstName = o.Customer.FirstName,
                        LastName = o.Customer.LastName,
                        PhoneNumber = o.Customer.PhoneNumber,
                        City = o.Customer.City,
                        Address = o.Customer.Address,
                        ZipCode = o.Customer.ZipCode
                    },
                    OrderId = o.Id,
                    OrderDate = o.OrderDate.ToString("f"),
                    OrderCost = o.TotalCost,
                    CustomerTotalOrders = repo.All<Order>()
                    .Where(or => or.Status != OrderStatus.Deleted)
                    .Count(or => or.CustomerId == o.CustomerId),
                    Products = products.Select(p => new ProductViewModel()
                    {
                        Name = p.Product.Name,
                        Price = p.Product.Price,
                        ImageUrl = p.Product.ImageUrl,
                        Quantity = p.Quantity

                    }).ToList()
                })
                .Where(o => o.OrderId == orderId)
                .FirstOrDefaultAsync();

            return order;
        }

        public async Task Delete(string orderId)
        {
            var order = await repo.All<Order>()
                .FirstOrDefaultAsync(o => o.Id == orderId);

            order.Status = OrderStatus.Deleted;

            repo.Update(order);
            await repo.SaveChangesAsync();
        }

        public async Task Process(string orderId)
        {
            var order = await repo.All<Order>()
                .FirstOrDefaultAsync(o => o.Id == orderId);

            order.Status = OrderStatus.Processed;

            repo.Update(order);
            await repo.SaveChangesAsync();
        }

    }
}


