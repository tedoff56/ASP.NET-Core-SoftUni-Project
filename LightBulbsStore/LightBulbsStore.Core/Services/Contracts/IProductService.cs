using LightBulbsStore.Core.Models.Category;
using LightBulbsStore.Core.Models.Product;

namespace LightBulbsStore.Core.Services.Contracts;

public interface IProductService
{
    Task AddProduct(ProductEditFormViewModel product);

    IEnumerable<ProductViewModel> GetAllProducts();

    IEnumerable<ProductViewModel> GetAllProducts(string id);

    Task<ProductEditFormViewModel> GetProduct(string productId);

    Task<bool> EditProduct(ProductEditFormViewModel model);
}