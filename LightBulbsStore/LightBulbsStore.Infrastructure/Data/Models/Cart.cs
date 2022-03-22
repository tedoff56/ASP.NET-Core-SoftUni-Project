using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LightBulbsStore.Infrastructure.Data.Models;

public class Cart
{
    public Cart()
    {
        this.Id = Guid.NewGuid();

        this.Products = new List<CartProduct>();
    }
    
    [Key]
    public Guid Id { get; set; }


    public Guid CustomerId { get; set; }
    
    [ForeignKey(nameof(CustomerId))]
    public Customer Customer { get; set; }
    
    public ICollection<CartProduct> Products { get; set; }
}