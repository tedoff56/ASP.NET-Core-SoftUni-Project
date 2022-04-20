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

        public async Task PlaceOrderAsync(OrderDetailsViewModel orderModel)
        {
            var order = await repo.GetByIdAsync<Order>(orderModel.OrderId);

            order.CustomerFirstName = orderModel.CustomerFirstName;
            order.CustomerLastName = orderModel.CustomerLastName;
            order.CustomerAddress = orderModel.CustomerAddress;
            order.CustomerCity = orderModel.CustomerCity;
            order.CustomerZipCode = orderModel.CustomerZipCode;
            order.CustomerPhoneNumber = orderModel.CustomerPhoneNumber;
            order.Status = OrderStatus.BeingProcessed;
            order.OrderDate = DateTime.UtcNow;

            repo.Update(order);
            await repo.SaveChangesAsync();

            await cartService.EmptyCartAsync(order.Customer.Cart.Id);
        }

        public async Task<OrderDetailsViewModel> CreateOrderAsync(string userId)
        {
            var user = await repo.GetByIdAsync<User>(userId);

            var cartProducts = (await cartService.GetCartProductsAsync(userId))
                .Select(cp => new OrderProductViewModel()
                {
                    ProductId = cp.ProductId,
                    Name = cp.Name,
                    ImageUrl = cp.ImageUrl,
                    CategoryName = cp.CategoryName,
                    Price = cp.Price,
                    Quantity = cp.Quantity,
                    TotalProductPrice = cp.TotalPrice
                })
                .ToList();


            var orderId = Guid.NewGuid().ToString();

            var order = new Order()
            {
                Id = orderId,
                TotalCost = cartProducts.Sum(p => p.Quantity * p.Price),
                Status = OrderStatus.NotFinished,
                CustomerId = user.Customer.Id,
                CustomerFirstName = user.Customer.FirstName is null ? string.Empty : user.Customer.FirstName,
                CustomerLastName = user.Customer.LastName is null ? string.Empty : user.Customer.LastName,
                CustomerAddress = user.Customer.Address is null ? string.Empty : user.Customer.Address,
                CustomerCity = user.Customer.City is null ? string.Empty : user.Customer.City,
                CustomerZipCode = user.Customer.ZipCode is null ? string.Empty : user.Customer.ZipCode,
                CustomerPhoneNumber = user.Customer.PhoneNumber is null ? string.Empty : user.Customer.PhoneNumber,
                OrderDate = DateTime.UtcNow,
                Products = cartProducts
                    .Select(p => new OrderProduct()
                    {
                        ProductId = p.ProductId,
                        Quantity = p.Quantity
                    })
                    .ToList()
            };

            user.Customer.Orders.Add(order);

            repo.Update(user);
            await repo.SaveChangesAsync();

            var orderProducts = order.Products
                .Select(op => new OrderProductViewModel()
                {
                    ProductId = op.Product.Id,
                    Name = op.Product.Name,
                    ImageUrl = op.Product.ImageUrl,
                    CategoryName = op.Product.Category.Name,
                    Price = op.Product.Price,
                    Quantity = op.Quantity
                })
                .ToList();

            return new OrderDetailsViewModel()
            {
                OrderId = orderId,
                TotalCost = order.TotalCost,
                Status = order.Status,
                CustomerFirstName = order.CustomerFirstName,
                CustomerLastName = order.CustomerLastName,
                CustomerAddress = order.CustomerAddress,
                CustomerCity = order.CustomerCity,
                CustomerZipCode = order.CustomerZipCode,
                CustomerPhoneNumber = order.CustomerPhoneNumber,
                OrderDate = order.OrderDate,
                Products = orderProducts
            };

        }

        public async Task<OrderDetailsViewModel> GetCartOrderDetailsAsync(string userId)
        {
            var customer = (await repo.GetByIdAsync<User>(userId)).Customer;

            var products = customer.Cart.Products
                .Select(p => new OrderProductViewModel()
                {
                    ProductId = p.Product.Id,
                    Name = p.Product.Name,
                    ImageUrl = p.Product.ImageUrl,
                    CategoryName = p.Product.Category.Name,
                    Price = p.Product.Price,
                    Quantity = p.Quantity,
                    TotalProductPrice = p.Product.Price * p.Quantity
                })
                .ToList();

            var orderDetails = new OrderDetailsViewModel()
            {
                Products = products,
                CustomerFirstName = customer.FirstName,
                CustomerLastName = customer.LastName,
                CustomerAddress = customer.Address,
                CustomerCity = customer.City,
                CustomerZipCode = customer.ZipCode,
                CustomerPhoneNumber = customer.PhoneNumber,
                TotalCost = products.Sum(p => p.TotalProductPrice)
            };

            return orderDetails;
        }


        public async Task<List<OrderAdminViewModel>> GetAllOrdersAsync()
        {
            var dbOrders = await repo.AllReadonly<Order>()
                .Where(o => o.Status == OrderStatus.BeingProcessed)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            var orders = new List<OrderAdminViewModel>();

            foreach (var order in dbOrders)
            {
                var totalOrders = dbOrders.Count();

                var user = await repo.All<User>()
                    .FirstOrDefaultAsync(u => u.Customer.Orders.Any(o => o.Id == order.Id));

                orders.Add(new OrderAdminViewModel
                {
                    UserEmail = user.UserName,
                    OrderId = order.Id,
                    OrderDate = order.OrderDate.ToString("f"),
                    TotalOrders = totalOrders,
                    OrderCost = order.TotalCost,
                });
            }

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
                        FirstName = o.CustomerFirstName,
                        LastName = o.CustomerLastName,
                        PhoneNumber = o.CustomerPhoneNumber,
                        City = o.CustomerCity,
                        Address = o.CustomerAddress,
                        ZipCode = o.CustomerZipCode
                    },
                    OrderId = o.Id,
                    OrderDate = o.OrderDate.ToString("f"),
                    OrderCost = o.TotalCost,
                    CustomerTotalOrders = repo.All<Order>()
                                .Where(or => or.Status != OrderStatus.Deleted && or.Status != OrderStatus.NotFinished)
                                .Count(or => or.CustomerId == o.CustomerId),
                    Products = products
                                .Select(p => new ProductViewModel()
                                {
                                    ProductId = p.Product.Id,
                                    Name = p.Product.Name,
                                    Price = p.Product.Price,
                                    ImageUrl = p.Product.ImageUrl,
                                    Quantity = p.Quantity
                                })
                                .ToList()
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

        public async Task<List<OrderProductViewModel>> GetOrderProductsAsync(string orderId)
        {
            var order = await repo.GetByIdAsync<Order>(orderId);

            var products = order.Products
                .Select(op => new OrderProductViewModel()
                {
                    ProductId = op.Product.Id,
                    Name = op.Product.Name,
                    ImageUrl = op.Product.ImageUrl,
                    CategoryName = op.Product.Category.Name,
                    Price = op.Product.Price,
                    Quantity = op.Quantity,
                    TotalProductPrice = op.Product.Price * op.Quantity
                })
                .ToList();

            return products;
        }
    }
}


