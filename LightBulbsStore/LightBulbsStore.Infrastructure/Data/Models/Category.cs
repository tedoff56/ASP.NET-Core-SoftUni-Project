using System.ComponentModel.DataAnnotations;
using LightBulbsStore.Data.Models;

namespace LightBulbsStore.Infrastructure.Data.Models;

public class Category
{
    public Category()
    {
        this.Products = new List<Product>();
    }
    
    public int Id { get; init; }

    [Required]
    [MaxLength(DataConstants.CategoryNameMaxLength)]
    public string Name { get; set; }

    public ICollection<Product> Products { get; init; }
}