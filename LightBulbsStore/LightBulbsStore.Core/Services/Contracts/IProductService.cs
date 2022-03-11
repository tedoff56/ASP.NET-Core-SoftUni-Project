using LightBulbsStore.Core.Models.Product;
using LightBulbsStore.Models.Product;

namespace LightBulbsStore.Core.Services.Contracts;

public interface IProductService
{ 
    IEnumerable<CategoryViewModel> GetCategories();

    void AddProduct(ProductAddFormViewModel product);

    IEnumerable<ProductViewModel> GetAllProducts();
}