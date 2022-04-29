using LightBulbsStore.Core.Services.Contracts;
using LightBulbsStore.Core.Services.Models.ContactForm;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MailKit.Net.Smtp;

namespace LightBulbsStore.Core.Services
{
    public class MailKitEmailService : IEmailService
    {
        //private readonly EmailServerConfiguration _eConfig;
        private  IConfiguration config;

        public MailKitEmailService(
            //EmailServerConfiguration config,
            IConfiguration _config)
        {
            config = _config;
        }

        public async Task SendEmailAsync(EmailMessage msg)
        {
            var configuration = config.GetSection("Smtp");
            var server = configuration.GetSection("SmtpServer").Value;
            var port = int.Parse(configuration.GetSection("SmtpPort").Value);
            var username = configuration.GetSection("SmtpUsername").Value;
            var password = configuration.GetSection("SmtpPassword").Value;

            var message = new MimeMessage();
            message.To.AddRange(msg.ToAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));
            message.From.AddRange(msg.FromAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));

            message.Subject = msg.Subject;

            message.Body = new TextPart("plain")
            {
                Text = msg.Content
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(server, port);

                client.AuthenticationMechanisms.Remove("XOAUTH2");

                await client.AuthenticateAsync(username, password);

                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
    }
}
