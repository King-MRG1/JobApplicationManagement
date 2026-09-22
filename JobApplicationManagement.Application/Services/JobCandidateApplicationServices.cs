using JobApplicationManagement.Application.Dtos.JobApplicationDto;
using JobApplicationManagement.Application.Interfaces;
using JobApplicationManagement.Domain.Entities;
using JobApplicationManagement.Domain.Enums;
using JobApplicationManagement.Domain.Exceptions;
namespace JobApplicationManagement.Application.Services
{
    public class JobCandidateApplicationServices
    {
        private readonly IGenericRepository<JobCandidateApplication> _applicationRepository;
        private readonly IGenericRepository<Job> _jobRepository;
        public JobCandidateApplicationServices(
            IGenericRepository<JobCandidateApplication> applicationRepository,
            IGenericRepository<Job> jobRepository)
        {
            _applicationRepository = applicationRepository;
            _jobRepository = jobRepository;
        }
        public async Task<JobApplicationResponseDto> CreateAsync(CreateJobApplicationDto dto, int candidateId)
        {
            var job = await _jobRepository.GetByIdAsync(dto.JobId)
                ?? throw new DomainException($"Job with id {dto.JobId} was not found.");
            if (!job.IsActive)
                throw new DomainException($"Job with id {dto.JobId} is no longer active and cannot accept applications.");
            var existingApplication = await _applicationRepository.FindFirstAsync(a => a.CandidateId == candidateId && a.JobId == dto.JobId);
            if (existingApplication != null)
                throw new DomainException($"You have already applied for job with id {dto.JobId}.");
            var now = DateTime.UtcNow;
            var application = new JobCandidateApplication
            {
                CandidateId = candidateId,
                JobId = dto.JobId,
                JobApplicationStatus = JobApplicationStatus.Applied,
                AppliedAt = now,
                StatusUpdatedAt = now
            };
            await _applicationRepository.AddAsync(application);
            await _applicationRepository.SaveChangesAsync();
            return MapToDto(application);
        }
        /// <summary>
        /// Cancels an existing application.
        /// Only allowed while the status is <see cref="JobApplicationStatus.Applied"/> or
        /// <see cref="JobApplicationStatus.UnderReview"/>; throws a <see cref="DomainException"/>
        /// for any other status.
        /// </summary>
        public async Task<JobApplicationResponseDto> CancelApplicationAsync(int applicationId, int candidateId)
        {
            var application = await _applicationRepository.GetByIdAsync(applicationId)
                ?? throw new DomainException($"Application with id {applicationId} was not found.");
            if (application.CandidateId != candidateId)
            {
                throw new DomainException("You are not authorised to cancel this application.");
            }
            if (application.JobApplicationStatus != JobApplicationStatus.Applied &&
                application.JobApplicationStatus != JobApplicationStatus.UnderReview)
            {
                throw new DomainException(
                    $"Application cannot be cancelled when its status is '{application.JobApplicationStatus}'. " +
                    "Only 'Applied' or 'UnderReview' applications can be cancelled.");
            }
            application.JobApplicationStatus = JobApplicationStatus.Cancelled;
            application.StatusUpdatedAt = DateTime.UtcNow;
            _applicationRepository.Update(application);
            await _applicationRepository.SaveChangesAsync();
            return MapToDto(application);
        }
        public async Task<JobApplicationResponseDto?> GetByIdAsync(int id)
        {
            var application = await _applicationRepository.GetByIdAsync(id);
            if (application is null) return null;
            return MapToDto(application);
        }
        private static JobApplicationResponseDto MapToDto(JobCandidateApplication app) => new()
        {
            Id = app.Id,
            CandidateId = app.CandidateId,
            JobId = app.JobId,
            Status = app.JobApplicationStatus,
            AppliedAt = app.AppliedAt,
            StatusUpdatedAt = app.StatusUpdatedAt
        };
    }
}