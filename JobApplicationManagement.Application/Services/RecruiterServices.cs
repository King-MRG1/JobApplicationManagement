using JobApplicationManagement.Application.Interfaces;
using JobApplicationManagement.Domain.Entities;
namespace JobApplicationManagement.Application.Services
{
    public class RecruiterServices
    {
        private readonly IGenericRepository<Recruiter> _recruiterRepository;
        public RecruiterServices(IGenericRepository<Recruiter> recruiterRepository)
        {
            _recruiterRepository = recruiterRepository;
        }
        /// <summary>
        /// Creates a Recruiter domain record linked to an existing ApplicationUser.
        /// Called immediately after a Recruiter user is registered.
        /// </summary>
        public async Task<Recruiter> CreateAsync(string name, string userId)
        {
            var recruiter = new Recruiter
            {
                Name = name,
                UserId = userId
            };
            await _recruiterRepository.AddAsync(recruiter);
            await _recruiterRepository.SaveChangesAsync();
            return recruiter;
        }
        public async Task<Recruiter?> GetByIdAsync(int id)
            => await _recruiterRepository.GetByIdAsync(id);
        /// <summary>
        /// Finds a recruiter by their linked ApplicationUser id.
        /// Used at login time to embed the RecruiterId claim into the JWT.
        /// </summary>
        public async Task<Recruiter?> GetByUserIdAsync(string userId)
            => await _recruiterRepository.FindFirstAsync(r => r.UserId == userId);
    }
}