using LightBulbsStore.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightBulbsStore.Core.Models.Customer
{
    public class CustomerInfoViewModel
    {
        [MaxLength(DataConstants.CustomerNameMaxLength)]
        public string FirstName { get; set; }

        [MaxLength(DataConstants.CustomerNameMaxLength)]
        public string LastName { get; set; }

        [Phone]
        [MaxLength(DataConstants.CustomerPhoneNumberMaxLength)]
        public string PhoneNumber { get; set; }

        [MaxLength(DataConstants.CustomerCityNameMaxLength)]
        public string City { get; set; }

        [MaxLength(DataConstants.CustomerAddressMaxLength)]
        public string Address { get; set; }

        [RegularExpression(@"\d{4}")]
        [MaxLength(DataConstants.CustomerZipCodeMaxLength)]
        public string? ZipCode { get; set; }

    }
}
