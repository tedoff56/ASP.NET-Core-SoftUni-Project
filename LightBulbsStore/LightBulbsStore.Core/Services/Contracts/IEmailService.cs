using LightBulbsStore.Core.Services.Models.ContactForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightBulbsStore.Core.Services.Contracts
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailMessage message);
    }
}
