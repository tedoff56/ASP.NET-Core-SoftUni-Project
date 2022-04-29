using LightBulbsStore.Core.Models.Customer;
using LightBulbsStore.Core.Models.Product;
using LightBulbsStore.Infrastructure.Data.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightBulbsStore.Core.Models.Order
{
    public class OrderDetailsAdminViewModel
    {
        public string OrderId { get; set; }

        public string UserEmail { get; set; }

        public DateTime OrderDate { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public string CustomerFirstName { get; set; }

        public string CustomerLastName { get; set; }

        public string CustomerCity { get; set; }

        public int TotalOrders { get; set; }

        public int ProductsCount { get; set; }

        public List<ProductViewModel> Products { get; set; }

        public CustomerInfoViewModel Customer { get; set; }

        public decimal OrderCost { get; set; }

        public int  CustomerTotalOrders { get; set; }
    }
}
