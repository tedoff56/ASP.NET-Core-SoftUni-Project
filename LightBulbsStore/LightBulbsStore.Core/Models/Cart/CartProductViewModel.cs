using LightBulbsStore.Core.Models.Category;
using LightBulbsStore.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightBulbsStore.Core.Models.Cart
{
    public class CartProductViewModel
    {
        public string ProductId { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string CategoryName { get; set; }

        public decimal Price { get; set; }

        [Range(0, 100)]
        [Required(ErrorMessage = DataConstants.RequiredFieldError)]
        [Display(Name = "Количество")]
        public int Quantity { get; set; }

        public decimal TotalPrice => Price * Quantity;

    }
}
