using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightBulbsStore.Core.Models.Cart
{
    public class CartViewModel
    {
        public CartViewModel()
        {
            Products = new List<CartProductViewModel>();
        }

        public string CartId { get; set; }

        public List<CartProductViewModel> Products { get; set; }

        public decimal TotalPrice { get; set; }

        public bool IsEmpty => Products.Count() == 0;
    }
}
