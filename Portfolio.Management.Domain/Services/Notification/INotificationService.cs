namespace Portfolio.Management.Domain.Services.Notification
{
    public interface INotificationService
    {
        Task NotifyAdminsOfExpiringProductsAsync();
    }
}