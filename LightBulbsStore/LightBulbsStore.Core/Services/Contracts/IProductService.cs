using LightBulbsStore.Core.Models.Category;
using LightBulbsStore.Core.Models.Product;

namespace LightBulbsStore.Core.Services.Contracts;

public interface IProductService
{ 
    IEnumerable<CategoryViewModel> GetCategories();

    void AddProduct(ProductAddFormViewModel product);

    IEnumerable<ProductViewModel> GetAllProducts();
}