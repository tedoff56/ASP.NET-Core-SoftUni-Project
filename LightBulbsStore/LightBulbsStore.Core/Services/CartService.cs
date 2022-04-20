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

        public async Task<List<CartProductViewModel>> GetCartProductsAsync(string userId)
        {
            var user = await repo.GetByIdAsync<User>(userId);

            var products = user.Customer.Cart.Products
                .Where(p => p.Quantity > 0)
                .Select(p => new CartProductViewModel()
                {
                    ProductId = p.ProductId,
                    Name = p.Product.Name,
                    ImageUrl = p.Product.ImageUrl,
                    CategoryName = p.Product.Category.Name,
                    Price = p.Product.Price,
                    Quantity = p.Quantity
                })
                .ToList();

            return products;
        }

        public async Task<CartViewModel> GetCartModelAsync(string userId)
        {
            var user = await repo.GetByIdAsync<User>(userId);

            return new CartViewModel()
            {
                Products = await GetCartProductsAsync(userId),
                TotalPrice = user.Customer.Cart.TotalPrice
            };
        }

        public async Task<bool> AddProductAsync(AddProductServiceModel model)
        {

            var user = await repo.All<User>()
                .FirstOrDefaultAsync(u => u.Id == model.UserId);

            var productToAdd = await repo.All<Product>()
                .FirstOrDefaultAsync(p => p.Id == model.ProductId);


            if (productToAdd is null || model.Quantity < 1)
            {
                return false;
            }

            var cartProduct = user.Customer.Cart.Products
                .FirstOrDefault(cp => cp.ProductId == model.ProductId);

            if (cartProduct is null)
            {
                user.Customer.Cart.Products.Add(new CartProduct
                {
                    ProductId = model.ProductId,
                    Quantity = model.Quantity
                });
            }
            else
            {
                cartProduct.Quantity += model.Quantity;
            }

            //user.Customer.Cart.TotalPrice += productToAdd.Price * model.Quantity;
            repo.Update(user);
            await repo.SaveChangesAsync();

            return true;
        }

        public async Task<int> TotalCartItemsAsync(string userId)
        {
            var products = await GetCartProductsAsync(userId);

            return products.Sum(p => p.Quantity);
        }

        public async Task<decimal> TotalPrice(string userId)
        {
            var products = await GetCartProductsAsync(userId);

            return products.Sum(p => p.Quantity * p.Price);
        }

        public async Task RemoveProductAsync(string userId, string productId)
        {

            var user = await repo.GetByIdAsync<User>(userId);

            var cartProduct = user.Customer.Cart.Products
                .FirstOrDefault(cp => cp.ProductId == productId);

            if(cartProduct == null)
            {
                return;
            }

            //user.Customer.Cart.TotalPrice -= cartProduct.Quantity * cartProduct.Product.Price;
            cartProduct.Quantity = 0;

            repo.Update(user);
            await repo.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateProductQuantityServiceModel model)
        {
            if (model.Quantity == 0)
            {
                return;
            }

            var user = await repo.GetByIdAsync<User>(model.UserId);

            var product = user.Customer.Cart.Products.FirstOrDefault(cp => cp.Product.Id == model.ProductId);

            if(product is null)
            {
                return;
            }      

            product.Quantity = model.Quantity;

            repo.Update(user);
            await repo.SaveChangesAsync();
        }

        public async Task EmptyCartAsync(string cartId)
        {
            var cart = await repo.GetByIdAsync<Cart>(cartId);

            var cartProducts = cart.Products
                .Where(cp => cp.Quantity > 0);

            foreach (var product in cartProducts)
            {
                product.Quantity = 0;
            }

            //cart.TotalPrice = 0;
            repo.Update(cart);
            await repo.SaveChangesAsync();
        }

        public async Task<bool> IsEmpty(string userId)
        {
            var products = await GetCartProductsAsync(userId);

            return products is null || !products.Any() ? true : false;
        }

        public async Task<string> GetCartIdAsync(string userId)
        {
            return (await repo.GetByIdAsync<User>(userId)).Customer.Cart.Id;
        }

    }
}
