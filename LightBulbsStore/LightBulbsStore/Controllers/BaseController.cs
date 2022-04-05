using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LightBulbsStore.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {

    }
}
