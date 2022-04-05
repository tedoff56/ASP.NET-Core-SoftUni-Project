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

        public IViewComponentResult Invoke() 
        {
            var result = categoryService.GetAllCategories();

            return View(result);
        }
    }
}
