using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LightBulbsStore.Infrastructure.Data.Models;

public class Product
{
    public Product()
    {
        this.Id = Guid.NewGuid();
    }
    
    [Key]
    public Guid Id { get; set; }

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
    
    public int CategoryId { get; set; }
    
    [ForeignKey(nameof(CategoryId))]
    public Category Category { get; set; }




}