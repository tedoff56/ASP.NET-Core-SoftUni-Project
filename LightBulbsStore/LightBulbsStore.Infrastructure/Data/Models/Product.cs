using LightBulbsStore.Infrastructure.Data.Enumerations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LightBulbsStore.Infrastructure.Data.Models;

public class Product
{
    public Product()
    {
        Id = Guid.NewGuid().ToString();
        Status = ProductStatus.Active;
    }
    
    public string Id { get; set; }

    [MaxLength(DataConstants.ProductNameMaxLength)]
    public string Name { get; set; }

    public decimal Price { get; set; }
    
    [MaxLength(DataConstants.ProductDescriptionMaxLength)]
    public string Description { get; set; }
    
    [MaxLength(DataConstants.UrlMaxLength)]
    public string ImageUrl { get; set; }
    
    public int CategoryId { get; set; }

    [ForeignKey(nameof(CategoryId))]
    public Category Category { get; set; }

    public List<CartProduct> Carts { get; set; }

    public List<OrderProduct> Orders { get; set; }

    public ProductStatus Status { get; set; }




}