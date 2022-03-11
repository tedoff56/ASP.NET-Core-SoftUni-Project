using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LightBulbsStore.Infrastructure.Data;
using LightBulbsStore.Infrastructure.Data.Models;

namespace LightBulbsStore.Data.Models;

public class Product
{
    [Key]
    public int Id { get; init; }

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
    public Category Category { get; init; }




}