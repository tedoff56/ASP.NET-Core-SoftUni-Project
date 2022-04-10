using LightBulbsStore.Core.Models.Category;
using LightBulbsStore.Core.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace LightBulbsStore.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService _categoryService)
        {
            categoryService = _categoryService;
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CategoryCreateViewModel categoryModel)
        {
            bool createdSuccessfully = await categoryService.CreateCategoryAsync(categoryModel);

            if (!ModelState.IsValid || !createdSuccessfully)
            {
                return this.View();
            }

            return this.Redirect("~/Product/");
        }
    }
}
