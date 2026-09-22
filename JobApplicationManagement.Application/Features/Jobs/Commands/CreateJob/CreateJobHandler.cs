using JobApplicationManagement.Application.Interfaces;
using JobApplicationManagement.Domain.Entities;
using MediatR;

namespace JobApplicationManagement.Application.Features.Jobs.CreateJob
{
    public class CreateJobHandler : IRequestHandler<CreateJobCommand, int>
    {
        private readonly IGenericRepository<Job> _jobRepository;

        public CreateJobHandler(IGenericRepository<Job> jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<int> Handle(CreateJobCommand request, CancellationToken cancellationToken)
        {
            var job = new Job
            {
                Title = request.Title,
                Description = request.Description,
                IsActive = true,
                RecruiterId = request.recruiterId

            };
            await _jobRepository.AddAsync(job);
            await _jobRepository.SaveChangesAsync();
            return job.Id;
        }
    }
}
