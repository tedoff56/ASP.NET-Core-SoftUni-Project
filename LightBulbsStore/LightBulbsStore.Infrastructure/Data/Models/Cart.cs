using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LightBulbsStore.Infrastructure.Data.Models;

public class Cart
{
    public Cart()
    {
        Id = Guid.NewGuid().ToString();

        CartProducts = new List<CartProduct>();

        Total = 0M;
    }

    [Key]
    public string Id { get; set; }
    
    [Required]
    public string CustomerId { get; set; }

    [ForeignKey(nameof(CustomerId))]
    public Customer Customer { get; set; }

    public decimal Total { get; set; }

    public ICollection<CartProduct> CartProducts { get; set; }
}