using JobApplicationManagement.Application.Dtos.JobApplicationDto;
using JobApplicationManagement.Application.Interfaces;
using JobApplicationManagement.Domain.Entities;
using JobApplicationManagement.Domain.Enums;
using JobApplicationManagement.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

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

        public async Task<JobApplicationResponseDto> CreateAsync(CreateJobApplicationDto createApplicationDto)
        {
            // Verify job exists and is active
            var job = await _jobRepository.GetByIdAsync(createApplicationDto.JobId);
            if (job == null)
            {
                throw new DomainException("Job does not exist.");
            }

            if (!job.IsActive)
            {
                throw new DomainException("Cannot apply to an inactive job.");
            }

            var application = new JobCandidateApplication()
            {
                CandidateId = createApplicationDto.CandidateId,
                JobId = createApplicationDto.JobId,
                JobApplicationStatus = JobApplicationStatus.Applied,
                AppliedAt = DateTime.UtcNow,
                StatusUpdatedAt = DateTime.UtcNow
            };

            await _applicationRepository.AddAsync(application);
            await _applicationRepository.SaveChangesAsync();

            return MapToResponseDto(application);
        }

        public async Task<JobApplicationResponseDto> CancelApplicationAsync(int applicationId)
        {
            var application = await _applicationRepository.GetByIdAsync(applicationId);
            if (application == null)
            {
                throw new DomainException("Application does not exist.");
            }

            if (application.JobApplicationStatus != JobApplicationStatus.Applied && 
                application.JobApplicationStatus != JobApplicationStatus.UnderReview)
            {
                throw new DomainException("Application cannot be cancelled in its current status.");
            }

            application.JobApplicationStatus = JobApplicationStatus.Cancelled;
            application.StatusUpdatedAt = DateTime.UtcNow;

            _applicationRepository.Update(application);
            await _applicationRepository.SaveChangesAsync();

            return MapToResponseDto(application);
        }

        private JobApplicationResponseDto MapToResponseDto(JobCandidateApplication application)
        {
            return new JobApplicationResponseDto
            {
                Id = application.Id,
                CandidateId = application.CandidateId,
                JobId = application.JobId,
                Status = application.JobApplicationStatus,
                AppliedAt = application.AppliedAt,
                StatusUpdatedAt = application.StatusUpdatedAt
            };
        }
    }
}
