using LightBulbsStore.Core.Models.Category;
using LightBulbsStore.Core.Models.Product;

namespace LightBulbsStore.Core.Services.Contracts;

public interface IProductService
{
    Task AddProductAsync(ProductEditFormViewModel product);

    Task<IEnumerable<ProductViewModel>> GetAllProductsAsync(string id);

    Task<ProductEditFormViewModel> GetProductForEditAsync(string productId);

    Task<bool> EditProductAsync(ProductEditFormViewModel model);

    Task<ProductViewModel> GetProductAsync(string productId);
}