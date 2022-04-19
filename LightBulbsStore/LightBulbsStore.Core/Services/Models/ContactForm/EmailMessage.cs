using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightBulbsStore.Core.Services.Models.ContactForm
{
    public class EmailMessage
    {
        public EmailMessage()
        {
            ToAddresses = new();
            FromAddresses = new();
        }

        public List<EmailAddress> ToAddresses { get; set; }

        public List<EmailAddress> FromAddresses { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }
    }
}
