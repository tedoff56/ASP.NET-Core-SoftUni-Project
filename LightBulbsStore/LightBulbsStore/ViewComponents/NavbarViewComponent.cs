using LightBulbsStore.Core.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace LightBulbsStore.ViewComponents
{
    public class NavbarViewComponent : ViewComponent
    {
        private readonly ICategoryService categoryService;

        public NavbarViewComponent(ICategoryService _categoryService)
        {
            categoryService = _categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync() 
        {
            var result = await categoryService.GetAllCategoriesAsync();

            return View(result);
        }
    }
}
