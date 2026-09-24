using Hangfire;
using Hangfire.SqlServer;
using JobApplicationManagement.Application.Interfaces;
using JobApplicationManagement.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobApplicationManagement.Infrastructure
{
    public static class HangfireExtensions
    {
        public static IServiceCollection AddHangfireInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' was not found.");

            services.AddHangfire(config => config
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(connectionString, new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                }));

            services.AddHangfireServer(options =>
            {
                options.ServerName = "JobApplicationManagement-Server";
                options.WorkerCount = Environment.ProcessorCount * 2;
            });

            services.AddScoped<IBackgroundJobService, HangfireBackgroundJobService>();
            services.AddScoped<INotificationJobService, NotificationJobService>();

            return services;
        }
    }
}
