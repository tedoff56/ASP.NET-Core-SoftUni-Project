using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightBulbsStore.Core.Models.Order
{
    public class OrderProductViewModel
    {
        public string ProductId { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string CategoryName { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public decimal TotalProductPrice { get; set; }

    }
}
