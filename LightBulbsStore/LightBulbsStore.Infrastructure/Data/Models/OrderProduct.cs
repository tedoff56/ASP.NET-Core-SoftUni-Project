using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LightBulbsStore.Infrastructure.Data.Models;

public class OrderProduct
{
    public string OrderId { get; set; }

    [ForeignKey(nameof(OrderId))]
    public Order Order { get; set; }

    public string ProductId { get; set; }

    [ForeignKey(nameof(ProductId))]
    public Product Product { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }
}