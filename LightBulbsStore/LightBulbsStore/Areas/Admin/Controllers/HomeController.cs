using Microsoft.AspNetCore.Mvc;

namespace LightBulbsStore.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
