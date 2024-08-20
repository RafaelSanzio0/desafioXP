using Microsoft.Extensions.Logging;
using Portfolio.Management.Domain.Configuration;
using Portfolio.Management.Domain.Services.Notification;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Portfolio.Management.Domain.Services.Notification
{
    public class EmailService(ILogger<EmailService> logger, EmailConfiguration emailConfiguration) : IEmailService
    {
        readonly ILogger<EmailService> _logger = logger;
        readonly EmailConfiguration _emailConfiguration = emailConfiguration;

        public async Task SendEmailAsync(string email, string emailBody)
        {
            var client = new SendGridClient(_emailConfiguration.Client);
            var message = new SendGridMessage()
            {
                From = new EmailAddress(_emailConfiguration.FromEmail, _emailConfiguration.FromName),
                Subject = _emailConfiguration.Subject,
                PlainTextContent = emailBody,
            };
            message.AddTo(new EmailAddress(email));
            await client.SendEmailAsync(message);
            _logger.LogInformation("Sent email for {0} with content {1}", email, emailBody);
        }
    }
}
