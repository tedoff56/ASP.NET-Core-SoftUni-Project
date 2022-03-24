using LightBulbsStore.Core.Models.Category;
using LightBulbsStore.Core.Models.Product;
using LightBulbsStore.Core.Services.Contracts;
using LightBulbsStore.Infrastructure.Data;
using LightBulbsStore.Infrastructure.Data.Models;
using LightBulbsStore.Infrastructure.Data.Repositories;
using System.Runtime.CompilerServices;

namespace LightBulbsStore.Services;

public class ProductService : IProductService
{
    private readonly IBulbsStoreDbRepository repo;

    public ProductService(IBulbsStoreDbRepository _repo)
    {
        repo = _repo;
    }

    public async Task AddProduct(ProductAddFormViewModel model)
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

    public IEnumerable<ProductViewModel> GetAllProducts(int categoryId)
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