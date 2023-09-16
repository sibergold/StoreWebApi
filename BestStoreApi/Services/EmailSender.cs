using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;

namespace BestStoreApi.Services
{
    public class EmailSender
    {
        private readonly string apiKey;
        private readonly string fromEmail;
        private readonly string senderName;

        public EmailSender(IConfiguration configuration)
        {
            apiKey = configuration["EmailSender:ApiKey"]!;
            fromEmail = configuration["EmailSender:FromEmail"]!;
            senderName = configuration["EmailSender:SenderName"]!;
        }

        public async Task SendEmail(string subject, string toEmail, string username, string message)
        {
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(fromEmail, senderName);
            var to = new EmailAddress(toEmail, username);
            var plainTextContent = message;
            var htmlContent = "";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
