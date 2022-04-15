using LightBulbsStore.Core.Models.Customer;
using LightBulbsStore.Core.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightBulbsStore.Core.Models.Order
{
    public class OrderAdminViewModel
    {
        public string OrderId { get; set; }

        public string UserEmail { get; set; }

        public string OrderDate { get; set; }

        public int TotalOrders { get; set; }

        public int ProductsCount { get; set; }

        public List<ProductViewModel> Products { get; set; }

        public CustomerInfoViewModel Customer { get; set; }

        public decimal OrderCost { get; set; }

        public int  CustomerTotalOrders { get; set; }
    }
}
