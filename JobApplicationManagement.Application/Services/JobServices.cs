using JobApplicationManagement.Application.Dtos.JobDto;
using JobApplicationManagement.Application.Interfaces;
using JobApplicationManagement.Domain.Entities;
using JobApplicationManagement.Domain.Exceptions;
namespace JobApplicationManagement.Application.Services
{
    public class JobServices
    {
        private readonly IGenericRepository<Job> _jobRepository;
        public JobServices(IGenericRepository<Job> jobRepository)
        {
            _jobRepository = jobRepository;
        }
        public async Task<JobResponseDto?> GetByIdAsync(int id)
        {
            var job = await _jobRepository.GetByIdAsync(id);
            if (job is null) return null;
            return MapToDto(job);
        }
        public async Task<JobResponseDto> CreateAsync(CreateJobDto createJobDto, int recruiterId)
        {
            var job = new Job
            {
                Title = createJobDto.Title,
                Description = createJobDto.Description,
                IsActive = true,
                RecruiterId = recruiterId

            };
            await _jobRepository.AddAsync(job);
            await _jobRepository.SaveChangesAsync();
            return MapToDto(job);
        }
        public async Task CloseJobAsync(int jobId, int recruiterId)
        {
            var job = await _jobRepository.GetByIdAsync(jobId)
                ?? throw new DomainException($"Job with id {jobId} was not found.");
            if (job.RecruiterId != recruiterId)
                throw new DomainException("You are not authorised to close this job.");
            job.IsActive = false;
            _jobRepository.Update(job);
            await _jobRepository.SaveChangesAsync();
        }
        private static JobResponseDto MapToDto(Job job) => new()
        {
            Id = job.Id,
            Title = job.Title,
            Description = job.Description,
            IsActive = job.IsActive,
            RecruiterId = job.RecruiterId
        };
    }
}