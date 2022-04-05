using LightBulbsStore.Core.Models.Category;
using LightBulbsStore.Core.Models.Product;

namespace LightBulbsStore.Core.Services.Contracts
{
    public interface ICategoryService
    {
        Task<bool> CreateCategory(CategoryCreateViewModel categoryModel);

        IEnumerable<CategoryViewModel> GetAllCategories();
    }
}
