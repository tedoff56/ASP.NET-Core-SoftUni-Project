using LightBulbsStore.Core.Models.Cart;
using LightBulbsStore.Core.Models.Customer;
using LightBulbsStore.Infrastructure.Data.Enumerations;

namespace LightBulbsStore.Core.Models.Order
{
    public class OrderDetailsViewModel
    {
        public int OrderId { get; set; }

        public List<CartProductViewModel> Products { get; set; }

        public decimal TotalCost { get; set; }

        public CustomerInfoViewModel Customer { get; set; }

        public DateTime OrderDate { get; set; }

    }
}
