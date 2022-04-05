using LightBulbsStore.Core.Models.Category;
using LightBulbsStore.Core.Models.Product;
using LightBulbsStore.Core.Services.Contracts;
using LightBulbsStore.Infrastructure.Data.Models;
using LightBulbsStore.Infrastructure.Data.Repositories;

namespace LightBulbsStore.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IBulbsStoreDbRepository repo;

        public CategoryService(IBulbsStoreDbRepository _repo)
        {
            repo = _repo;
        }

        public async Task<bool> CreateCategory(CategoryCreateViewModel categoryModel)
        {
            bool categoryAlreadyExists = repo.All<Category>().Any(c => c.Name.ToUpper() == categoryModel.Name.ToUpper());

            if (categoryAlreadyExists)
            {
                return false;
            }

            await repo.AddAsync(new Category()
            {
                Name = categoryModel.Name,
                Description = categoryModel.Description
            });

            await repo.SaveChangesAsync();

            return true;
        }

        public IEnumerable<CategoryViewModel> GetAllCategories()
        {
            return repo.All<Category>()
                .Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description
                })
                .ToList();
        }
    }
}
