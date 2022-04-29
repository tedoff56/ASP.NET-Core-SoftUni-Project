using LightBulbsStore.Core.Models.Category;
using LightBulbsStore.Core.Models.Product;

namespace LightBulbsStore.Core.Services.Contracts;

public interface IProductService
{
    Task AddProductAsync(ProductEditFormViewModel product);

    Task<List<ProductViewModel>> GetAllProductsAsync(int? categoryId = null);

    Task<ProductEditFormViewModel> GetProductForEditAsync(string productId);

    Task<bool> EditProductAsync(ProductEditFormViewModel model);

    Task<ProductViewModel> GetProductAsync(string productId);

    Task<List<ProductViewModel>> SearchForProduct(string text);
}