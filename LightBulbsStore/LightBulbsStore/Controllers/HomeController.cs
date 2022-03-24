using System.Diagnostics;
using LightBulbsStore.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace LightBulbsStore.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> logger;

    public HomeController(
        ILogger<HomeController> _logger)
    {
        logger = _logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}