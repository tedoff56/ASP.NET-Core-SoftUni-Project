using LightBulbsStore.Core.Models.Category;
using LightBulbsStore.Core.Models.Product;

namespace LightBulbsStore.Core.Services.Contracts
{
    public interface ICategoryService
    {
        Task<bool> CreateCategoryAsync(CategoryCreateViewModel categoryModel);

        Task<IEnumerable<CategoryViewModel>> GetAllCategoriesAsync();
    }
}
