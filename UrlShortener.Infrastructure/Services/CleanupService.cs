using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using UrlShortener.Infrastructure.Persistence;

namespace UrlShortener.Infrastructure.Services
{
    public class CleanupService : IHostedService, IDisposable
    {
        private readonly int CleanUpTimeSpanDays = 3;
        private readonly ILogger<CleanupService> _logger;
        private Timer _timer;
        private readonly IServiceProvider _serviceProvider;

        public CleanupService(IServiceProvider serviceProvider, ILogger<CleanupService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Cleanup Service is starting.");
            _timer = new Timer(this.DoWork, null, TimeSpan.Zero, TimeSpan.FromDays(CleanUpTimeSpanDays));
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var expiredEntries = context.Urls.Where(u => u.ExpirationDate <= DateTime.UtcNow).ToList();

                if (expiredEntries.Any())
                {
                    context.Urls.RemoveRange(expiredEntries);
                    context.SaveChanges();
                    _logger.LogInformation($"Removed {expiredEntries.Count} expired entries.");
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Cleanup Service is stopping.");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
