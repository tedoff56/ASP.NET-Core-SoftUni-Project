using LightBulbsStore.Infrastructure.Data;
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

        [Required(ErrorMessage = DataConstants.RequiredFieldError)]
        [EmailAddress]
        [Display(Name = "Имейл")]
        [RegularExpression(DataConstants.EmailValidationRegex, 
            ErrorMessage = "Невалиден имейл")]
        public string Email { get; set; }

        [Required(ErrorMessage = DataConstants.RequiredFieldError)]
        [StringLength(DataConstants.ContactSubjectMaxLength, 
            MinimumLength = DataConstants.ContactSubjectMinLength,
            ErrorMessage = DataConstants.MinMaxFieldError)]
        [Display(Name = "Заглавие")]
        public string Subject { get; set; }

        [Required(ErrorMessage = DataConstants.RequiredFieldError)]
        [Display(Name = "Съобщение")]
        public string Message { get; set; }

    }
}
