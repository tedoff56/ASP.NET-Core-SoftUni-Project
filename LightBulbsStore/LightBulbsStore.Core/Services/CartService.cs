using LightBulbsStore.Core.Models.Cart;
using LightBulbsStore.Core.Models.Product;
using LightBulbsStore.Core.Services.Contracts;
using LightBulbsStore.Infrastructure.Data.Models;
using LightBulbsStore.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightBulbsStore.Core.Services
{
    public class CartService : ICartService
    {
        private readonly IBulbsStoreDbRepository repo;

        private readonly IUserService userService;

        private readonly UserManager<User> userManager;

        public CartService(
            IBulbsStoreDbRepository _repo,
            IUserService _userService,
            UserManager<User> _userManager)
        {
            repo = _repo;
            userService = _userService;
            userManager = _userManager;
        }

        public async Task<IEnumerable<CartProductViewModel>> GetProducts(string userId)
        {
            var customer = await repo.All<Customer>()
                .Where(c => c.UserId == userId)
                .FirstOrDefaultAsync();

            var cart = await repo.All<Cart>()
                .Where(c => c.CustomerId == customer.Id)
                .Include(c => c.CartProducts)
                .ThenInclude(cp => cp.Product)
                .ThenInclude(p => p.Category)
                .FirstOrDefaultAsync();

            return cart.CartProducts
                .Select(p => new CartProductViewModel()
                {
                    Id = p.ProductId,
                    Name = p.Product.Name,
                    ImageUrl = p.Product.ImageUrl,
                    CategoryName = p.Product.Category.Name,
                    Price = p.Product.Price,
                    Quantity = p.Quantity
                })
                .Where(p => p.Quantity > 0);

        }

        public async Task<bool> AddProduct(string productId, string userId)
        {
            var customer = await repo.All<Customer>()
                .Where(c => c.UserId == userId)
                .FirstOrDefaultAsync();

            var cart = await repo.All<Cart>()
                .Where(c => c.CustomerId == customer.Id)
                .Include(c => c.CartProducts)
                .FirstOrDefaultAsync();

            var product = repo.All<Product>()
                .Where(p => p.Id == productId)
                .FirstOrDefault();

            if (product is null)
            {
                return false;
            }

            var cartProduct = cart.CartProducts
                .FirstOrDefault(cp => cp.CartId == cart.Id && cp.ProductId == productId);

            if (cartProduct is null)
            {
                cart.CartProducts.Add(new CartProduct
                {
                    CartId = cart.Id,
                    ProductId = productId,
                    Quantity = 1
                });
            }
            else
            {
                cartProduct.Quantity += 1;
            }

            repo.Update(cart);
            await repo.SaveChangesAsync();

            return true;
        }

        public async Task<int> TotalCartItems(string userId)
        {
            var products = await GetProducts(userId);

            return products.Select(p => p.Quantity).Sum();
        }

        public async Task<decimal> TotalPrice(string userId)
        {
            var products = await GetProducts(userId);

            return products.Sum(p => p.Quantity * p.Price);
        }

        public async Task UpdateCart(CartViewModel cartViewModel)
        {
            var cart = await repo.All<Cart>()
                .Where(c => c.Id == cartViewModel.Id)
                .Include(c => c.CartProducts)
                .ThenInclude(cp => cp.Product)
                .FirstOrDefaultAsync();

            if (cart == null)
            {
                return;
            }

            foreach(var product in cartViewModel.Products)
            {
                var cartProduct = cart.CartProducts
                    .Where(cp => cp.ProductId == product.Id)
                    .FirstOrDefault();

                cartProduct.Quantity = product.Quantity;

                repo.Update(cartProduct);
            }

            await repo.SaveChangesAsync();
        }
    }
}
