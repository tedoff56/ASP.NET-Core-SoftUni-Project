using System.ComponentModel.DataAnnotations;
using LightBulbsStore.Core.Models.Category;
using LightBulbsStore.Infrastructure.Data;

namespace LightBulbsStore.Core.Models.Product;

public class ProductEditFormViewModel
{
    public ProductEditFormViewModel()
    {
        this.Categories = new HashSet<CategoryViewModel>();
    }

    public string? Id { get; set; }

    [Required]
    [StringLength(
        DataConstants.ProductNameMaxLength, 
        MinimumLength = DataConstants.ProductNameMinLength, 
        ErrorMessage = "{0} трябва да съдържа между {2} и {1} символа!")]
    [Display(Name = "Име на продукт")]
    public string Name { get; init; }
    
    [Required]
    [Range(0.01, 10_000, ErrorMessage = "Цената на продукта трябва да е в диапазон от {1} до {2}")]
    [Display(Name = "Цена")]
    public decimal Price { get; init; }
    
    [Required]
    [StringLength(
        DataConstants.ProductDescriptionMaxLength,
        MinimumLength = DataConstants.ProductDescriptionMinLength,
        ErrorMessage = "Описанието трябва да съдържа между {2} и {1} символа!")]
    [Display(Name = "Описание")]
    public string Description { get; init; }
    
    [Required]
    [Url(ErrorMessage = "Невалиднен URL!")]
    [Display(Name = "Изображение")]
    public string ImageUrl { get; init; }
    
    [Required]
    [Display(Name = "Категория")]
    public string CategoryId { get; init; }

    public IEnumerable<CategoryViewModel> Categories { get; set; }
}