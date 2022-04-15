using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LightBulbsStore.Infrastructure.Data.Enumerations;

namespace LightBulbsStore.Infrastructure.Data.Models;

public class Order
{
    public Order()
    {
        Id = Guid.NewGuid().ToString();

        Products = new List<OrderProduct>();

        Status = OrderStatus.BeingProcessed;
    }
    
    [Key]
    public string Id { get; set; }

    public decimal TotalCost { get; set; }

    public OrderStatus Status { get; set; }

    //public string CustomerId { get; set; }

    //[Required]
    //[ForeignKey(nameof(CustomerId))]
    //public Customer Customer { get; set; }

    public string CustomerFirstName { get; set; }

    [MaxLength(DataConstants.CustomerNameMaxLength)]
    public string CustomerLastName { get; set; }

    [MaxLength(DataConstants.CustomerPhoneNumberMaxLength)]
    public string CustomerPhoneNumber { get; set; }

    [MaxLength(DataConstants.CustomerCityNameMaxLength)]
    public string CustomerCity { get; set; }

    [MaxLength(DataConstants.CustomerAddressMaxLength)]
    public string CustomerAddress { get; set; }

    [MaxLength(DataConstants.CustomerZipCodeMaxLength)]
    public string CustomerZipCode { get; set; }

    public ICollection<OrderProduct> Products { get; set; }

    public DateTime OrderDate { get; set; }
}