using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LightBulbsStore.Infrastructure.Data.Models;

public class CartProduct
{

    public CartProduct()
    {
        Quantity = 0;
    }

    [Key]
    public int Id { get; set; }

    [Required]
    public string CartId { get; set; }

    [ForeignKey(nameof(CartId))]
    public Cart Cart { get; set; }

    [Required]
    public string ProductId { get; set; }

    [ForeignKey(nameof(ProductId))]
    public Product Product { get; set; }

    public int Quantity { get; set; }

}