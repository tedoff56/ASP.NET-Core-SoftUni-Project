using System.ComponentModel.DataAnnotations;

namespace LightBulbsStore.Data.Models;

public class Product
{
    public Product()
    {
        this.Id = Guid.NewGuid().ToString();
    }
    
    [Key]
    public string Id { get; init; }

    [Required]
    public string Name { get; init; }

    public decimal Price { get; init; }
    
    [Required]
    public string Description { get; init; }
    
    [Required]
    [MaxLength(2048)]
    public string ImageUrl { get; init; }




}