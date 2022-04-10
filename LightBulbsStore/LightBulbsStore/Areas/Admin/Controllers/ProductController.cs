
using LightBulbsStore.Controllers;
using LightBulbsStore.Core.Models.Product;
using LightBulbsStore.Core.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
namespace LightBulbsStore.Areas.Admin.Controllers;

public class ProductController : BaseController
{
    private readonly IProductService productService;

    private readonly ICategoryService categoryService;

    public ProductController(
        IProductService _productService,
        ICategoryService _categoryService)
    {
        productService = _productService;
        categoryService = _categoryService;
    }

    public async Task<IActionResult> Add()
    {
        return View(new ProductEditFormViewModel()
        {
            Categories = await categoryService.GetAllCategoriesAsync()
        });
    }

    [HttpPost]
    public async Task<IActionResult> Add(ProductEditFormViewModel product)
    {
        bool categoryExist = (await categoryService.GetAllCategoriesAsync())
            .Any(c => c.Id == product.CategoryId);

        if (!categoryExist)
        {
            ModelState.AddModelError(nameof(product.CategoryId), "Invalid category!");
        }

        if (!ModelState.IsValid)
        {
            product.Categories = await categoryService.GetAllCategoriesAsync();
            return this.View(product);
        }

        await productService.AddProductAsync(product);

        return Redirect("~/Product/");
    }

    public async Task<IActionResult> Edit(string productId)
    {
        var product = await productService.GetProductForEditAsync(productId);

        return View(product);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(string productId, ProductEditFormViewModel productModel)
    {

        productModel.Categories = await categoryService.GetAllCategoriesAsync();
        productModel.Id = productId;

        if (!ModelState.IsValid)
        {
            return View(productModel);
        }
        var editedSuccessfully = await productService.EditProductAsync(productModel);

        if (!editedSuccessfully)
        {
            return View(productModel);
        }

        return RedirectToAction("Info", "Product", new { area = "/", @productId = productId });
    }



}