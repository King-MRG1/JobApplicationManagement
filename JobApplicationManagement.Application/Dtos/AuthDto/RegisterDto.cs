using System.ComponentModel.DataAnnotations;
namespace JobApplicationManagement.Application.Dtos.AuthDto
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string FullName { get; set; } = string.Empty;
        /// <summary>
        /// "Recruiter" or "Candidate" — determines which linked domain record is created.
        /// </summary>
        [Required]
        public string Role { get; set; } = string.Empty;
    }
}