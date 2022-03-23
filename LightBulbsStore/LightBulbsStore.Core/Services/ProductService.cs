using LightBulbsStore.Core.Models.Category;
using LightBulbsStore.Core.Models.Product;
using LightBulbsStore.Core.Services.Contracts;
using LightBulbsStore.Infrastructure.Data;
using LightBulbsStore.Infrastructure.Data.Models;

namespace LightBulbsStore.Services;

public class ProductService : IProductService
{
    private readonly BulbsStoreDbContext dbContext;
    
    public ProductService(BulbsStoreDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
    IEnumerable<CategoryViewModel> IProductService.GetCategories()
        => dbContext.Categories
            .Select(c => new CategoryViewModel()
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToList();

    public void AddProduct(ProductAddFormViewModel model)
    {
        var product = new Product()
        {
            Name = model.Name,
            Price = model.Price,
            Description = model.Description,
            ImageUrl = model.ImageUrl,
            CategoryId = model.CategoryId
        };

        dbContext.Products.Add(product);
        dbContext.SaveChanges();
    }

    public IEnumerable<ProductViewModel> GetAllProducts()
        => dbContext.Products
            .Select(p => new ProductViewModel()
            {
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                ImageUrl = p.ImageUrl,
                CategoryName = p.Category.Name
                
            })
            .ToList();
}