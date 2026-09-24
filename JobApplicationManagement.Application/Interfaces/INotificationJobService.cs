namespace JobApplicationManagement.Application.Interfaces
{
    public interface INotificationJobService
    {
        Task SendApplicationNotificationAsync(int applicationId, string message);
        Task ProcessApplicationReviewReminderAsync(int applicationId);
        Task RunDailyJobCleanupAsync();
    }
}
