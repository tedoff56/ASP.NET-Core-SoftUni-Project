using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LightBulbsStore.Infrastructure.Data.Models;

public class OrderProduct
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string OrderId { get; set; }
    
    [ForeignKey(nameof(OrderId))]
    public Order Order { get; set; }

    [Required]
    public string ProductId { get; set; }
    
    [ForeignKey(nameof(ProductId))]
    public Product Product { get; set; }

    public int Quantity { get; set; }
}