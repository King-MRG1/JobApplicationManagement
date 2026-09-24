using Hangfire;
using JobApplicationManagement.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BackgroundJobsController : ControllerBase
    {
        private readonly IBackgroundJobService _backgroundJobService;

        public BackgroundJobsController(IBackgroundJobService backgroundJobService)
        {
            _backgroundJobService = backgroundJobService;
        }

        /// <summary>
        /// Enqueues a fire-and-forget background job.
        /// </summary>
        [HttpPost("fire-and-forget")]
        public IActionResult TriggerFireAndForget([FromQuery] string message = "Welcome to JobApplicationManagement!")
        {
            var jobId = _backgroundJobService.Enqueue<INotificationJobService>(service =>
                service.SendApplicationNotificationAsync(0, message));

            return Ok(new
            {
                JobId = jobId,
                Type = "FireAndForget",
                Message = $"Job enqueued successfully. Check Hangfire dashboard at /hangfire to inspect execution."
            });
        }

        /// <summary>
        /// Schedules a delayed background job.
        /// </summary>
        [HttpPost("delayed")]
        public IActionResult TriggerDelayed([FromQuery] int delayInSeconds = 30)
        {
            var jobId = _backgroundJobService.Schedule<INotificationJobService>(
                service => service.ProcessApplicationReviewReminderAsync(0),
                TimeSpan.FromSeconds(delayInSeconds));

            return Ok(new
            {
                JobId = jobId,
                Type = "Delayed",
                ScheduledAt = DateTime.UtcNow.AddSeconds(delayInSeconds),
                Message = $"Delayed job scheduled to run in {delayInSeconds} seconds. Check /hangfire dashboard."
            });
        }

        /// <summary>
        /// Registers or updates a recurring background job.
        /// </summary>
        [HttpPost("recurring")]
        public IActionResult RegisterRecurringJob([FromQuery] string cronExpression = Cron.Daily)
        {
            const string recurringJobId = "daily-job-application-cleanup";

            _backgroundJobService.AddOrUpdateRecurringJob<INotificationJobService>(
                recurringJobId,
                service => service.RunDailyJobCleanupAsync(),
                cronExpression);

            return Ok(new
            {
                RecurringJobId = recurringJobId,
                CronExpression = cronExpression,
                Message = "Recurring job registered successfully. Check /hangfire dashboard under Recurring Jobs."
            });
        }

        /// <summary>
        /// Deletes a background job by ID.
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult DeleteJob(string id)
        {
            var isDeleted = _backgroundJobService.Delete(id);
            return Ok(new { JobId = id, Deleted = isDeleted });
        }
    }
}
