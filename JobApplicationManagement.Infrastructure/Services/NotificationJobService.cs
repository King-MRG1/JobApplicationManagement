using JobApplicationManagement.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace JobApplicationManagement.Infrastructure.Services
{
    public class NotificationJobService : INotificationJobService
    {
        private readonly ILogger<NotificationJobService> _logger;

        public NotificationJobService(ILogger<NotificationJobService> logger)
        {
            _logger = logger;
        }

        public async Task SendApplicationNotificationAsync(int applicationId, string message)
        {
            _logger.LogInformation("Processing background notification for Application #{ApplicationId}: {Message}", applicationId, message);
            await Task.Delay(500);
            _logger.LogInformation("Successfully processed background notification for Application #{ApplicationId}", applicationId);
        }

        public async Task ProcessApplicationReviewReminderAsync(int applicationId)
        {
            _logger.LogInformation("Sending review reminder for Application #{ApplicationId}", applicationId);
            await Task.Delay(500);
        }

        public async Task RunDailyJobCleanupAsync()
        {
            _logger.LogInformation("Executing recurring job: Daily application & job maintenance check at {Time}", DateTime.UtcNow);
            await Task.CompletedTask;
        }
    }
}
