using LightBulbsStore.Core.Models.Cart;
using LightBulbsStore.Core.Services.Contracts;
using LightBulbsStore.Core.Services.Models.Cart;
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

        public async Task<IEnumerable<CartProductViewModel>> GetProductsAsync(string userId)
        {
            var customer = await repo.All<Customer>()
                .Where(c => c.UserId == userId)
                .FirstOrDefaultAsync();

            var cart = await repo.All<Cart>()
                .Where(c => c.CustomerId == customer.Id)
                .FirstOrDefaultAsync();

            var products = cart.CartProducts
                .Where(p => p.Quantity > 0)
                .Select(p => new CartProductViewModel()
                {
                    Id = p.ProductId,
                    Name = p.Product.Name,
                    ImageUrl = p.Product.ImageUrl,
                    CategoryName = p.Product.Category.Name,
                    Price = p.Product.Price,
                    Quantity = p.Quantity
                });

            return products;
        }

        public async Task<bool> AddProductAsync(AddProductServiceModel model)
        {
            var customer = await repo.All<Customer>()
                .Where(c => c.UserId == model.UserId)
                .FirstOrDefaultAsync();

            var cart = await repo.All<Cart>()
                .Where(c => c.CustomerId == customer.Id)
                .FirstOrDefaultAsync();

            var product = repo.All<Product>()
                .Where(p => p.Id == model.ProductId)
                .FirstOrDefault();

            if (product is null || model.Quantity < 1)
            {
                return false;
            }

            var cartProduct = cart.CartProducts
                .FirstOrDefault(cp => cp.CartId == cart.Id && cp.ProductId == model.ProductId);

            if (cartProduct is null)
            {
                cart.CartProducts.Add(new CartProduct
                {
                    CartId = cart.Id,
                    ProductId = model.ProductId,
                    Quantity = model.Quantity
                });
            }
            else
            {
                cartProduct.Quantity += model.Quantity;
            }

            repo.Update(cart);
            await repo.SaveChangesAsync();

            return true;
        }

        public async Task<int> TotalCartItemsAsync(string userId)
        {
            var products = await GetProductsAsync(userId);

            return products.Select(p => p.Quantity).Sum();
        }

        public async Task<decimal> TotalPrice(string userId)
        {
            var products = await GetProductsAsync(userId);

            return products.Sum(p => p.Quantity * p.Price);
        }

        public async Task UpdateCartAsync(CartViewModel cartViewModel, string userId)
        {
            var cartId = await GetCartIdAsync(userId);

            if (cartId == null)
            {
                return;
            }

            var cart = await repo.All<Cart>()
                .Where(c => c.Id == cartId)
                .FirstOrDefaultAsync();

            foreach(var product in cart.CartProducts)
            {
                var quantityToSet = cartViewModel.Products
                    .FirstOrDefault(p => p.Id == product.ProductId)
                    .Quantity;


                product.Quantity = quantityToSet;

                repo.Update(cart);
            }

            //foreach (var product in cartViewModel.Products)
            //{
            //    var cartProduct = cart.CartProducts
            //        .Where(cp => cp.ProductId == product.Id)
            //        .FirstOrDefault();

            //    cartProduct.Quantity = product.Quantity;


            //}

            await repo.SaveChangesAsync();
        }

        public async Task<string> GetCartIdAsync(string userId)
        {
            var cart = await repo.All<Cart>()
                .Where(c => c.Customer.UserId == userId)
                .FirstOrDefaultAsync();

            return cart.Id;
        }

        public async Task RemoveProductAsync(string userId, string productId)
        {
            var cart = await repo.All<Cart>()
                .Where(c => c.Customer.UserId == userId)
                .FirstOrDefaultAsync();

            var product = cart.CartProducts
                .Where(cp => cp.ProductId == productId)
                .FirstOrDefault();

            product.Quantity = 0;

            repo.Update(product);
            await repo.SaveChangesAsync();
        }

        public async Task UpdateAsync(CartViewModel model)
        {
            var cart = await repo.GetByIdAsync<Cart>(model.CartId);

            foreach (var product in model.Products)
            {
                var cartProduct = cart.CartProducts
                    .FirstOrDefault(cp => cp.ProductId == product.Id);

                cartProduct.Quantity = product.Quantity;

                repo.Update(cartProduct);
            }

            await repo.SaveChangesAsync();

        }

        public async Task EmptyCartAsync(string userId)
        {
            var cart = await repo.All<Cart>()
                .Where(c => c.Customer.UserId == userId)
                .Include(c => c.CartProducts)
                .ThenInclude(cp => cp.Product)
                .FirstOrDefaultAsync();

            var cartProducts = cart.CartProducts
                .Where(cp => cp.Quantity > 0);

            foreach (var product in cartProducts)
            {
                product.Quantity = 0;
            }

            repo.Update(cart);
            await repo.SaveChangesAsync();
        }

        public async Task<bool> IsEmpty(string userId)
        {
            var products = await GetProductsAsync(userId);

            return products is null || !products.Any() ? true : false;
        }
    }
}
