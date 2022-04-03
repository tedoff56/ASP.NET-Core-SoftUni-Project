using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LightBulbsStore.Infrastructure.Data.Models;

public class Product
{
    public Product()
    {
        Id = Guid.NewGuid().ToString();
    }
    
    [Key]
    public string Id { get; set; }

    [Required]
    [MaxLength(DataConstants.ProductNameMaxLength)]
    public string Name { get; set; }

    public decimal Price { get; set; }
    
    [Required]
    [MaxLength(DataConstants.ProductDescriptionMaxLength)]
    public string Description { get; set; }
    
    [Required]
    [MaxLength(DataConstants.UrlMaxLength)]
    public string ImageUrl { get; set; }
    
    public string CategoryId { get; set; }
    
    [ForeignKey(nameof(CategoryId))]
    public Category Category { get; set; }




}