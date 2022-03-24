using LightBulbsStore.Core.Models.Category;
using LightBulbsStore.Core.Models.Product;

namespace LightBulbsStore.Core.Services.Contracts;

public interface IProductService
{
    Task AddProduct(ProductAddFormViewModel product);

    IEnumerable<ProductViewModel> GetAllProducts();

    IEnumerable<ProductViewModel> GetAllProducts(int id);
}