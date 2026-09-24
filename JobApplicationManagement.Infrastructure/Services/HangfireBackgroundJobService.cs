using Hangfire;
using JobApplicationManagement.Application.Interfaces;
using System.Linq.Expressions;

namespace JobApplicationManagement.Infrastructure.Services
{
    public class HangfireBackgroundJobService : IBackgroundJobService
    {
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IRecurringJobManager _recurringJobManager;

        public HangfireBackgroundJobService(
            IBackgroundJobClient backgroundJobClient,
            IRecurringJobManager recurringJobManager)
        {
            _backgroundJobClient = backgroundJobClient;
            _recurringJobManager = recurringJobManager;
        }

        public string Enqueue(Expression<Action> methodCall) =>
            _backgroundJobClient.Enqueue(methodCall);

        public string Enqueue<T>(Expression<Action<T>> methodCall) =>
            _backgroundJobClient.Enqueue(methodCall);

        public string Enqueue(Expression<Func<Task>> methodCall) =>
            _backgroundJobClient.Enqueue(methodCall);

        public string Enqueue<T>(Expression<Func<T, Task>> methodCall) =>
            _backgroundJobClient.Enqueue(methodCall);

        public string Schedule(Expression<Action> methodCall, TimeSpan delay) =>
            _backgroundJobClient.Schedule(methodCall, delay);

        public string Schedule<T>(Expression<Action<T>> methodCall, TimeSpan delay) =>
            _backgroundJobClient.Schedule(methodCall, delay);

        public string Schedule(Expression<Func<Task>> methodCall, TimeSpan delay) =>
            _backgroundJobClient.Schedule(methodCall, delay);

        public string Schedule<T>(Expression<Func<T, Task>> methodCall, TimeSpan delay) =>
            _backgroundJobClient.Schedule(methodCall, delay);

        public void AddOrUpdateRecurringJob(string recurringJobId, Expression<Action> methodCall, string cronExpression) =>
            _recurringJobManager.AddOrUpdate(recurringJobId, methodCall, cronExpression);

        public void AddOrUpdateRecurringJob<T>(string recurringJobId, Expression<Action<T>> methodCall, string cronExpression) =>
            _recurringJobManager.AddOrUpdate(recurringJobId, methodCall, cronExpression);

        public void AddOrUpdateRecurringJob(string recurringJobId, Expression<Func<Task>> methodCall, string cronExpression) =>
            _recurringJobManager.AddOrUpdate(recurringJobId, methodCall, cronExpression);

        public void AddOrUpdateRecurringJob<T>(string recurringJobId, Expression<Func<T, Task>> methodCall, string cronExpression) =>
            _recurringJobManager.AddOrUpdate(recurringJobId, methodCall, cronExpression);

        public bool Delete(string jobId) =>
            _backgroundJobClient.Delete(jobId);
    }
}
