using LightBulbsStore.Core.Models.Cart;
using LightBulbsStore.Core.Models.Customer;
using LightBulbsStore.Infrastructure.Data;
using LightBulbsStore.Infrastructure.Data.Enumerations;
using System.ComponentModel.DataAnnotations;

namespace LightBulbsStore.Core.Models.Order
{
    public class OrderDetailsViewModel
    {
        public string OrderId { get; set; }

        public List<OrderProductViewModel> Products { get; set; }

        public decimal TotalCost { get; set; }

        public OrderStatus Status { get; set; }

        [Required]
        [Display(Name = "Име")]
        [MaxLength(DataConstants.CustomerNameMaxLength)]
        public string CustomerFirstName { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        [MaxLength(DataConstants.CustomerNameMaxLength)]
        public string CustomerLastName { get; set; }

        [Required]
        [Display(Name = "Адрес")]
        [MaxLength(DataConstants.CustomerAddressMaxLength)]
        public string CustomerAddress { get; set; }

        [Required]
        [Display(Name = "Град")]
        [MaxLength(DataConstants.CustomerCityNameMaxLength)]
        public string CustomerCity { get; set; }

        [Required]
        [Display(Name = "Пощенски код")]
        [RegularExpression(@"\d{4}", ErrorMessage = @"{0} трябва да съдържа точно 4 цифри")]
        public string CustomerZipCode { get; set; }

        [Required]
        [Display(Name = "Телефонен номер")]
        [MaxLength(DataConstants.CustomerPhoneNumberMaxLength)]
        public string CustomerPhoneNumber { get; set; }

        public DateTime OrderDate { get; set; }

    }
}
