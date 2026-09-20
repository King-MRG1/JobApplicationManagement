using Microsoft.AspNetCore.Identity;
namespace JobApplicationManagement.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
    }
}
