using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LightBulbsStore.Infrastructure.Data.Models;

public class Customer
{
    public Customer()
    {
        Id = Guid.NewGuid().ToString();

        Orders = new List<Order>();
    }
    
    [Key]
    [MaxLength(DataConstants.GuidIdMaxLength)]
    public string Id { get; init; }

    [MaxLength(DataConstants.CustomerNameMaxLength)]
    public string? FirstName { get; set; }
    
    [MaxLength(DataConstants.CustomerNameMaxLength)]
    public string? LastName { get; set; }
    
    [MaxLength(DataConstants.CustomerPhoneNumberMaxLength)]
    public string? PhoneNumber { get; set; }
    
    [MaxLength(DataConstants.CustomerCityNameMaxLength)]
    public string? City { get; set; }
    
    [MaxLength(DataConstants.CustomerAddressMaxLength)]
    public string? Address { get; set; }

    [MaxLength(DataConstants.CustomerZipCodeMaxLength)]
    public string? ZipCode { get; set; }

    [Required]
    public string UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; }

    public string CartId { get; set; }

    [ForeignKey(nameof(CartId))]
    public Cart Cart { get; set; }

    public ICollection<Order> Orders { get; set; }
    
}