using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightBulbsStore.Core.Services.Models.Cart
{
    public class AddProductServiceModel
    {
        public string ProductId { get; set; }

        public string UserId { get; set; }

        public int Quantity { get; set; }
    }
}
