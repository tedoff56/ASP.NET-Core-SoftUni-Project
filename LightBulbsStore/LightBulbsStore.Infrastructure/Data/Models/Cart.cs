using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LightBulbsStore.Infrastructure.Data.Models;

public class Cart
{
    public Cart()
    {
        Id = Guid.NewGuid().ToString();
    }

    public string Id { get; set; }

    public decimal TotalPrice { get; set; }

    public List<CartProduct> Products { get; set; }
}