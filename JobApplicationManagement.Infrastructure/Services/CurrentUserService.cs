using JobApplicationManagement.Application.Interfaces;
using JobApplicationManagement.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
namespace JobApplicationManagement.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string? UserId =>
            _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        public int? RecruiterId
        {
            get
            {
                var value = _httpContextAccessor.HttpContext?.User.FindFirstValue("RecruiterId");
                return int.TryParse(value, out var id) ? id : null;
            }
        }

        public int? CandidateId
        {
            get
            {
                var value = _httpContextAccessor.HttpContext?.User.FindFirstValue("CandidateId");
                return int.TryParse(value, out var id) ? id : null;
            }
        }
    }
}