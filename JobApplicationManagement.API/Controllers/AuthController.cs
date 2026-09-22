using JobApplicationManagement.Application.Dtos.AuthDto;
using JobApplicationManagement.Application.Dtos.CandidateDto;
using JobApplicationManagement.Application.Features.Candidates.Commands.CreateCandidate;
using JobApplicationManagement.Application.Features.Candidates.Queries.GetCandidateByUserId;
using JobApplicationManagement.Application.Features.Recruiters.Commands.CreateRecruiter;
using JobApplicationManagement.Application.Features.Recruiters.Queries.GetRecruiterByUserId;
using JobApplicationManagement.Application.Interfaces;
using JobApplicationManagement.Application.Services;
using JobApplicationManagement.Domain.Entities;
using MediatR;
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
        private readonly ITokenService _tokenService;
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ITokenService tokenService,
            IMediator mediator,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
            _mediator = mediator;
            _configuration = configuration;
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
            int? candidateId = null;
            if (normalizedRole == "Recruiter")
            {
                var recruiter = await _mediator.Send(new CreateRecruiterCommand
                {
                    Name = dto.FullName,
                    UserId = user.Id
                });
                recruiterId = recruiter.Id;
            }
            else
            {
               var candidate = await _mediator.Send(new CreateCandidateCommand
                {
                    Name = dto.FullName,
                    CvUrl = string.Empty,
                    UserId = user.Id
                });
                candidateId = candidate.Id;
            }
            var roles = await _userManager.GetRolesAsync(user);
            var token = _tokenService.GenerateToken(user, roles, recruiterId, candidateId);
            var expirationMinutes = int.Parse(_configuration["Jwt:ExpirationMinutes"] ?? "60");
            return Ok(new AuthResponseDto
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddMinutes(expirationMinutes)
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
                var recruiter = await _mediator.Send(new GetRecruiterByUserIdQuery { UserId = user.Id });
                recruiterId = recruiter?.Id;
            }
            int? candidateId = null;
            if (roles.Contains("Candidate"))
            {
                var candidate = await _mediator.Send(new GetCandidateByUserIdQuery { UserId = user.Id });
                candidateId = candidate?.Id;
            }
            var expirationMinutes = int.Parse(_configuration["Jwt:ExpirationMinutes"] ?? "60");
            var token = _tokenService.GenerateToken(user, roles, recruiterId, candidateId);
            return Ok(new AuthResponseDto
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddMinutes(expirationMinutes)
            });
        }
    }
}