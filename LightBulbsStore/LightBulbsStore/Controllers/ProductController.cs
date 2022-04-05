using LightBulbsStore.Core.Models.Product;
using LightBulbsStore.Core.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace LightBulbsStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;

        public ProductController(IProductService _productService)
        {
            productService = _productService;
        }

        public IActionResult Index(string categoryId)
        {
            var result = productService.GetAllProducts(categoryId);

            if (categoryId is null)
            {
                result = productService.GetAllProducts();
            }

            return View(result);
        }

        public async Task<IActionResult> Info(string productId)
        {
            var product = await productService.GetProduct(productId);

            if(product is null)
            {
                return View();
            }

            return View(product);

        }
    }
}
