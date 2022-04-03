using System.ComponentModel.DataAnnotations;

namespace LightBulbsStore.Infrastructure.Data.Models;

public class Category
{
    public Category()
    {
        Id = Guid.NewGuid().ToString();
        Products = new List<Product>();
    }
    
    [Key]
    public string Id { get; set; }

    [Required]
    [MaxLength(DataConstants.CategoryNameMaxLength)]
    public string Name { get; set; }
    
    [MaxLength(DataConstants.CategoryDescriptionMaxLength)]
    public string? Description { get; set; }
    
    public ICollection<Product> Products { get; set; }
}