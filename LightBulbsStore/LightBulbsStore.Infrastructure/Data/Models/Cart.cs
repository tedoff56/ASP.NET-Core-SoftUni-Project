using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LightBulbsStore.Infrastructure.Data.Models;

public class Cart
{
    public Cart()
    {
        Id = Guid.NewGuid().ToString();

        Products = new();
    }

    public string Id { get; set; }

    public List<CartProduct> Products { get; set; }

    [NotMapped]
    public decimal TotalPrice => Products.Sum(p => p.Quantity * p.Product.Price);
}