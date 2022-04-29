using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightBulbsStore.Core.Services.Models.ContactForm
{
    public class EmailMessage
    {
        private string name = "krushki.com";
        private string address = "krushki.com.contacts@gmail.com";

        public EmailMessage()
        {
            ToAddresses = new()
            {
                new EmailAddress()
                {
                    Name = name,
                    Address = address
                }
            };
            FromAddresses = new()
            {
                new EmailAddress()
                {
                    Name = name,
                    Address = address
                }
            };
        }

        public List<EmailAddress> ToAddresses { get; set; }

        public List<EmailAddress> FromAddresses { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }
    }
}
