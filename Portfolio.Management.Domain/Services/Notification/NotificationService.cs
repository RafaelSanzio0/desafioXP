using Microsoft.Extensions.Logging;
using Portfolio.Management.Domain.Entities;
using Portfolio.Management.Domain.Repositories;
using System.Text;

namespace Portfolio.Management.Domain.Services.Notification
{
    public class NotificationService(
        IEmailService emailService,
        IDataReadRepository dataReadRepository,
        ILogger<NotificationService> logger) : INotificationService
    {
        private readonly IEmailService _emailService = emailService;
        private readonly IDataReadRepository _dataReadRepository = dataReadRepository;
        private readonly ILogger<NotificationService> _logger = logger;

        public async Task NotifyAdminsOfExpiringProductsAsync()
        {
            var targetDate = DateTime.UtcNow.AddDays(7);
            var expiringProducts = await _dataReadRepository.GetExpiringFinancialProductsAsync(targetDate);

            if (expiringProducts.Any())
            {
                var adminEmails = await GetAdminEmailsAsync();

                foreach (var email in adminEmails)
                {
                    await _emailService.SendEmailAsync(email, CreateEmailBody(expiringProducts));
                }
            }
            else
            {
                _logger.LogInformation("No expiring products found.");
            }
        }

        private async Task<IEnumerable<string>> GetAdminEmailsAsync()
        {
            return await _dataReadRepository.GetAdminUsersAsync();
        }

        private static string CreateEmailBody(IEnumerable<FinancialProductEntity> products)
        {
            var sb = new StringBuilder();
            sb.AppendLine("The following financial products are nearing their expiration date:");

            foreach (var product in products)
            {
                sb.AppendLine($"- {product.Name} (Expires on: {product.ExpirationDate:yyyy-MM-dd})");
            }

            return sb.ToString();
        }
    }
}