using JobApplicationManagement.Application.Dtos.AuthDto;
using JobApplicationManagement.Application.Dtos.CandidateDto;
using JobApplicationManagement.Application.Services;
using JobApplicationManagement.Domain.Entities;
using JobApplicationManagement.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly TokenService _tokenService;
        private readonly RecruiterServices _recruiterServices;
        private readonly CandidateServices _candidateServices;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            TokenService tokenService,
            RecruiterServices recruiterServices,
            CandidateServices candidateServices)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
            _recruiterServices = recruiterServices;
            _candidateServices = candidateServices;
        }

        /// <summary>
        /// POST /api/auth/register
        /// Creates an ApplicationUser, assigns the requested role, and (based on role) creates
        /// a linked Recruiter or Candidate domain record.
        /// Returns a JWT on success.
        /// </summary>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            // Validate role
            var normalizedRole = dto.Role.Trim();
            if (normalizedRole != "Recruiter" && normalizedRole != "Candidate")
                return BadRequest(new { error = "Role must be 'Recruiter' or 'Candidate'." });
            // Create identity user
            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                FullName = dto.FullName
            };
            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            // Ensure the role exists
            if (!await _roleManager.RoleExistsAsync(normalizedRole))
                await _roleManager.CreateAsync(new IdentityRole(normalizedRole));
            await _userManager.AddToRoleAsync(user, normalizedRole);
            // Create linked domain record and embed RecruiterId in token if applicable
            int? recruiterId = null;
            if (normalizedRole == "Recruiter")
            {
                var recruiter = await _recruiterServices.CreateAsync(dto.FullName, user.Id);
                recruiterId = recruiter.Id;
            }
            else
            {
                await _candidateServices.CreateAsync(new CreateCandidateDto
                {
                    Name = dto.FullName,
                    CvUrl  = string.Empty,
                }, user.Id);
            }
            var roles = await _userManager.GetRolesAsync(user);
            var token = _tokenService.GenerateToken(user, roles, recruiterId);
            return Ok(new AuthResponseDto
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddMinutes(60) // mirrors TokenService default
            });
        }

        /// <summary>
        /// POST /api/auth/login
        /// Validates credentials and returns a JWT. Returns 401 on failure.
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user is null || !await _userManager.CheckPasswordAsync(user, dto.Password))
                return Unauthorized(new { error = "Invalid credentials." });
            var roles = await _userManager.GetRolesAsync(user);
            // If the user is a Recruiter, load their RecruiterId for the JWT claim
            int? recruiterId = null;
            if (roles.Contains("Recruiter"))
            {
                var recruiter = await _recruiterServices.GetByUserIdAsync(user.Id);
                recruiterId = recruiter?.Id;
            }
            var token = _tokenService.GenerateToken(user, roles, recruiterId);
            return Ok(new AuthResponseDto
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddMinutes(60)
            });
        }
    }
}