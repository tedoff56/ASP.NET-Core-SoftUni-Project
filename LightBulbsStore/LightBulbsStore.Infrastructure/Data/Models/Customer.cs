using System.ComponentModel.DataAnnotations;

namespace LightBulbsStore.Infrastructure.Data.Models;

public class Customer
{
    public Customer()
    {
        this.Id = Guid.NewGuid().ToString();
    }
    
    [Key]
    [MaxLength(DataConstants.GuidIdMaxLength)]
    public string Id { get; init; }
    
    [Required]
    [MaxLength(DataConstants.CustomerEmailAddressMaxLength)]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
    
    [Required]
    [MaxLength(DataConstants.CustomerNameMaxLength)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(DataConstants.CustomerNameMaxLength)]
    public string LastName { get; set; }

    [Required]
    [MaxLength(DataConstants.CustomerPhoneNumberMaxLength)]
    public string PhoneNumber { get; set; }
    
    [MaxLength(DataConstants.CustomerCityNameMaxLength)]
    public string? City { get; set; }
    
    [MaxLength(DataConstants.CustomerStreetAddressMaxLength)]
    public string? StreetAddress { get; set; }
    
}