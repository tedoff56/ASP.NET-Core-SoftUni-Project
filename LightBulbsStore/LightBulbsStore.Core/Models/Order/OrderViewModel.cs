using LightBulbsStore.Core.Models.Cart;
using LightBulbsStore.Core.Models.Customer;
using LightBulbsStore.Infrastructure.Data.Enumerations;

namespace LightBulbsStore.Core.Models.Order
{
    public class OrderViewModel
    {
        public List<CartProductViewModel> Products { get; set; }

        public decimal TotalCost => Products is null ? 0 : Products.Sum(p => p.Quantity * p.Price);

        public CustomerInfoViewModel Customer { get; set; }

    }
}
