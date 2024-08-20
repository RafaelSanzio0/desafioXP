namespace Portfolio.Management.Domain.Services.Notification
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string emailBody);
    }
}