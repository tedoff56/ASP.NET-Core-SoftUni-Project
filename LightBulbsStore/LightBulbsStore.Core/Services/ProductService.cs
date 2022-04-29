using LightBulbsStore.Core.Models.Category;
using LightBulbsStore.Core.Models.Product;
using LightBulbsStore.Core.Services.Contracts;
using LightBulbsStore.Core.Services.Models;
using LightBulbsStore.Infrastructure.Data;
using LightBulbsStore.Infrastructure.Data.Models;
using LightBulbsStore.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace LightBulbsStore.Services;

public class ProductService : IProductService
{
    private readonly IBulbsStoreDbRepository repo;

    private readonly ICategoryService categoryService;

    public ProductService(
        IBulbsStoreDbRepository _repo,
        ICategoryService _categoryService)
    {
        repo = _repo;
        categoryService = _categoryService;
    }

    public async Task AddProductAsync(ProductEditFormViewModel model)
    {
        var product = new Product()
        {
            Name = model.Name,
            Price = model.Price,
            Description = model.Description,
            ImageUrl = model.ImageUrl,
            CategoryId = model.CategoryId
        };

        await repo.AddAsync(product);
        await repo.SaveChangesAsync();
    }

    public async Task<ProductEditFormViewModel> GetProductForEditAsync(string productId)
    {
        var product = await repo.GetByIdAsync<Product>(productId);

        if (product is null)
        {
            return null;
        }

        return new ProductEditFormViewModel()
        {
            Name = product.Name,
            Price = product.Price,
            Description = product.Description,
            ImageUrl = product.ImageUrl,
            CategoryId = product.CategoryId,
            Categories = await categoryService.GetAllCategoriesAsync()
        };
    }

    public async Task<bool> EditProductAsync(ProductEditFormViewModel model)
    {
        var product = await repo.GetByIdAsync<Product>(model.Id);

        if (product is null)
        {
            return false;
        }

        product.Name = model.Name;
        product.Price = model.Price;
        product.Description = model.Description;
        product.ImageUrl = model.ImageUrl;
        product.CategoryId = model.CategoryId;

        repo.Update(product);
        await repo.SaveChangesAsync();

        return true;
    }

    public async Task<List<ProductViewModel>> GetAllProductsAsync(int? categoryId = null) 
    {
        var products = await repo.All<Product>()
            .Select(p => new ProductViewModel()
            {
                ProductId = p.Id,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                ImageUrl = p.ImageUrl,
                Category = new CategoryViewModel
                {
                    Id = p.Category.Id,
                    Name = p.Category.Name,
                    Description = p.Description,
                }

            }).ToListAsync();

        if (categoryId is null)
        {
            return products;
        }

        return products.Where(p => p.Category.Id == (int)categoryId).ToList();
    }

    public async Task<ProductViewModel> GetProductAsync(string productId)
    {
        var product = await repo.GetByIdAsync<Product>(productId);

        if (product is null)
        {
            return null;
        }

        var category = await repo.GetByIdAsync<Category>(product.CategoryId);

        return new ProductViewModel()
        {
            ProductId = product.Id,
            Name = product.Name,
            Category = new CategoryViewModel()
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            },
            Price = product.Price,
            ImageUrl = product.ImageUrl,
            Description = product.Description,
        };
    }

    public async Task<List<ProductViewModel>> SearchForProduct(string text)
    {
        var result = await GetAllProductsAsync(); ;


        return result
            .Where(p => p.Name.ToLower().Contains(text.ToLower()))
            .ToList();
    }
}