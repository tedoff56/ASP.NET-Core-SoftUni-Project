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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CategoryCreateViewModel categoryModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            bool createdSuccessfully = await categoryService.CreateCategoryAsync(categoryModel);

            if (!createdSuccessfully)
            {
                ModelState.AddModelError(string.Empty, "Категорията вече съществува");
                return View();
            }

            return Redirect("~/Product/");
        }

    }
}
