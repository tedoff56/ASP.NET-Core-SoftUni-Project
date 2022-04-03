using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LightBulbsStore.Infrastructure.Data.Models;

public class Cart
{
    public Cart()
    {
        Id = Guid.NewGuid().ToString();

        CartProducts = new List<CartProduct>();
    }

    [Key]
    public string Id { get; set; }

    public string CustomerId { get; set; }

    [ForeignKey(nameof(CustomerId))]
    public Customer Customer { get; set; }

    public ICollection<CartProduct> CartProducts { get; set; }
}