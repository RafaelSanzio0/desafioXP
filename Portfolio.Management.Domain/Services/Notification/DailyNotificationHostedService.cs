using Microsoft.Extensions.Hosting;

namespace Portfolio.Management.Domain.Services.Notification
{
    public class DailyNotificationHostedService(INotificationService notificationService) : IHostedService, IDisposable
    {
        private readonly INotificationService _notificationService = notificationService;
        private Timer _timer;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromDays(1));
            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            await _notificationService.NotifyAdminsOfExpiringProductsAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}