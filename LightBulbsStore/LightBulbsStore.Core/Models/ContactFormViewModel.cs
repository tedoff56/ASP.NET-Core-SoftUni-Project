using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightBulbsStore.Core.Models
{
    public class ContactFormViewModel
    {

        [Required]
        [EmailAddress]
        [Display(Name = "Имейл")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Заглавие")]
        public string Subject { get; set; }

        [Required]
        [Display(Name = "Съобщение")]
        public string Message { get; set; }

    }
}
