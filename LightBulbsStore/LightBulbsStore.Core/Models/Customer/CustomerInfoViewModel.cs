using LightBulbsStore.Infrastructure.Data;
using System.ComponentModel.DataAnnotations;

namespace LightBulbsStore.Core.Models.Customer
{
    public class CustomerInfoViewModel
    {
        [Required]
        [Display(Name = "Име")]
        [MaxLength(DataConstants.CustomerNameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        [MaxLength(DataConstants.CustomerNameMaxLength)]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Телефонен номер")]
        [MaxLength(DataConstants.CustomerPhoneNumberMaxLength)]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Град")]
        [MaxLength(DataConstants.CustomerCityNameMaxLength)]
        public string City { get; set; }

        [Required]
        [Display(Name = "Адрес")]
        [MaxLength(DataConstants.CustomerAddressMaxLength)]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Пощенски код")]
        [RegularExpression(@"\d{4}", ErrorMessage = @"{0} трябва да съдържа точно 4 цифри")]
        public string ZipCode { get; set; }
    }
}
