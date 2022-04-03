using LightBulbsStore.Core.Models.Category;
using LightBulbsStore.Core.Models.Product;
using LightBulbsStore.Core.Services.Contracts;
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

    public async Task AddProduct(ProductEditFormViewModel model)
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

    public async Task<ProductEditFormViewModel> GetProduct(string productId)
    {
        var product = await repo.GetByIdAsync<Product>(productId);

        if(product is null)
        {
            return new ProductEditFormViewModel();
        }

       return new ProductEditFormViewModel()
       {
           Name = product.Name,
           Price = product.Price,
           Description = product.Description,
           ImageUrl = product.ImageUrl,
           CategoryId = product.CategoryId,
           Categories = categoryService.GetAllCategories()
       } ;
    }
    
    public async Task<bool> EditProduct(ProductEditFormViewModel model)
    {
        var product = await repo.GetByIdAsync<Product>(model.Id);

        if(product is null)
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

    public IEnumerable<ProductViewModel> GetAllProducts()
        => repo.All<Product>()
            .Select(p => new ProductViewModel()
            {
                Id = p.Id,
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

            })
            .ToList();

    public IEnumerable<ProductViewModel> GetAllProducts(string categoryId)
    {
        return repo.All<Product>()
            .Where(p => p.CategoryId == categoryId)
            .Select(p => new ProductViewModel()
            {
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                ImageUrl = p.ImageUrl,
                Category = new CategoryViewModel
                {
                    Id = categoryId,
                    Name = p.Category.Name,
                    Description = p.Description,
                }
            })
            .ToList();
    }
}