using LightBulbsStore.Core.Models.Cart;
using LightBulbsStore.Core.Models.Customer;
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
        public string CustomerFirstName { get; set; }

        [Required]
        public string CustomerLastName { get; set; }

        [Required]
        public string CustomerAddress { get; set; }

        [Required]
        public string CustomerCity { get; set; }

        [Required]
        public string CustomerZipCode { get; set; }

        [Required]
        public string CustomerPhoneNumber { get; set; }

        public DateTime OrderDate { get; set; }

    }
}
