using LightBulbsStore.Models.Product;

namespace LightBulbsStore.Services.Contracts;

public interface IProductService
{ 
    IEnumerable<CategoryViewModel> GetCategories();

    void AddProduct(ProductAddFormViewModel product);

    IEnumerable<ProductViewModel> GetAllProducts();
}