using JobApplicationManagement.Application.Dtos.JobDto;
using JobApplicationManagement.Application.Interfaces;
using JobApplicationManagement.Domain.Entities;
using MediatR;

namespace JobApplicationManagement.Application.Features.Jobs.Queries.GetAllJob
{
    public class GetAllJobHandler : IRequestHandler<GetAllJobQuery, List<JobResponseDto>>
    {
        private readonly IGenericRepository<Job> _jobRepository;

        public GetAllJobHandler(IGenericRepository<Job> jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<List<JobResponseDto>> Handle(GetAllJobQuery request, CancellationToken cancellationToken)
        {
            var jobs = await _jobRepository.GetAsync();

            return jobs.Select(j => new JobResponseDto
            {
                Id = j.Id,
                Title = j.Title,
                Description = j.Description,
                IsActive = j.IsActive,
            }).ToList();
        }
    }
}
