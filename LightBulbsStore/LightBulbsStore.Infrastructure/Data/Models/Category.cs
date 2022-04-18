using System.ComponentModel.DataAnnotations;

namespace LightBulbsStore.Infrastructure.Data.Models;

public class Category
{
    public Category()
    {
        Products = new List<Product>();
    }

    public int Id { get; set; }

    [MaxLength(DataConstants.CategoryNameMaxLength)]
    public string Name { get; set; }
    
    [MaxLength(DataConstants.CategoryDescriptionMaxLength)]
    public string? Description { get; set; }
    
    public List<Product> Products { get; set; }
}