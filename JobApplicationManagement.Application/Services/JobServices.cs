using JobApplicationManagement.Application.Dtos.JobDto;
using JobApplicationManagement.Application.Interfaces;
using JobApplicationManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

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
            if (job == null)
            {
                return null;
            }

            return MapToResponseDto(job);
        }

        public async Task<JobResponseDto> CreateAsync(CreateJobDto createJobDto)
        {
            var job = new Job()
            {
                Title = createJobDto.Title,
                Description = createJobDto.Description,
                IsActive = true
            };

            await _jobRepository.AddAsync(job);
            await _jobRepository.SaveChangesAsync();

            return MapToResponseDto(job);
        }

        private JobResponseDto MapToResponseDto(Job job)
        {
            return new JobResponseDto
            {
                Id = job.Id,
                Title = job.Title,
                Description = job.Description,
                IsActive = job.IsActive
            };
        }
    }
}
