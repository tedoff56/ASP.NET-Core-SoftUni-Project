using System.Diagnostics;
using LightBulbsStore.Core.Models;
using Microsoft.AspNetCore.Mvc;
using LightBulbsStore.Core.Services.Contracts;

namespace LightBulbsStore.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> logger;

    private readonly ICategoryService categoryService;

    public HomeController(
        ILogger<HomeController> _logger,
        ICategoryService _categoryService)
    {
        logger = _logger;
        categoryService = _categoryService;
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