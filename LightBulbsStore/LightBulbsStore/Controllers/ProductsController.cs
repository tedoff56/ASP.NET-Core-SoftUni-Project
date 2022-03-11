using LightBulbsStore.Core.Models.Product;
using LightBulbsStore.Core.Services.Contracts;
using LightBulbsStore.Models;
using LightBulbsStore.Models.Product;
using Microsoft.AspNetCore.Mvc;

namespace LightBulbsStore.Controllers;

public class ProductsController : Controller
{
    private readonly IProductService productService;
    
    public ProductsController(IProductService productService)
    {
        this.productService = productService;
    }

    public IActionResult Add()
    {
        return this.View(new ProductAddFormViewModel()
        {
            Categories = productService.GetCategories()
        });
    }

    [HttpPost]
    public IActionResult Add(ProductAddFormViewModel product)
    {
        
        if(!productService.GetCategories().Any(c => c.Id == product.CategoryId))
        {
            ModelState.AddModelError(nameof(product.CategoryId), "Invalid category!");
        }
        
        if (!ModelState.IsValid)
        {
            product.Categories = productService.GetCategories();
            return this.View(product);
        }

        productService.AddProduct(product);
        
        return RedirectToAction("Index", "Home");
    }

    public IActionResult Index(ProductViewModel product)
    {
        product.Products = productService.GetAllProducts();
        
        return this.View(product);
    }
}