using Hangfire.Dashboard;

namespace JobApplicationManagement.API.Filters
{
    public class HangfireDashboardAuthorizationFilter : IDashboardAuthorizationFilter
    {
        private readonly IWebHostEnvironment _environment;

        public HangfireDashboardAuthorizationFilter(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();

            // Allow unrestricted access in local/development environment
            if (_environment.IsDevelopment())
            {
                return true;
            }

            // In production/staging, require authenticated user with Admin role
            return httpContext.User.Identity?.IsAuthenticated == true &&
                   httpContext.User.IsInRole("Admin");
        }
    }
}
