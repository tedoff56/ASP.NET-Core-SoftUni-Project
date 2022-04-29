using LightBulbsStore.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightBulbsStore.Core.Models.Category
{
    public class CategoryCreateViewModel
    {
        [Required(ErrorMessage = DataConstants.RequiredFieldError)]
        [StringLength(30, MinimumLength = 5, 
            ErrorMessage = DataConstants.MinMaxFieldError)]
        [Display(Name = "Име")]
        public string Name { get; set; }


        [Display(Name = "Описание")]
        public string? Description { get; set; }
    }
}
