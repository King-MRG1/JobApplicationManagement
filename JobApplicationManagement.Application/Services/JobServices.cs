using JobApplicationManagement.Application.Interfaces;
using JobApplicationManagement.Domain;
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

        public async Task<Job> GetByIdAsync(int id)
        {
            return await _jobRepository.GetByIdAsync(id);
        }
        public async Task<int> CreateAsync(CreateJobDto createJobDto)
        {
            var job = new Job()
            {
                Title = createJobDto.Title,
                Description = createJobDto.Description,
                IsActive = true
            };

            await _jobRepository.AddAsync(job);
            await _jobRepository.SaveChangesAsync();

            return job.Id;
        }
    }
}
