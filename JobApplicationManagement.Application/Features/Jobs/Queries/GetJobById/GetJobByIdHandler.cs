using JobApplicationManagement.Application.Dtos.JobDto;
using JobApplicationManagement.Application.Interfaces;
using JobApplicationManagement.Domain.Entities;
using MediatR;

namespace JobApplicationManagement.Application.Features.Jobs.Queries.GetJobById
{
    public class GetJobByIdHandler : IRequestHandler<GetJobByIdQuery, JobResponseDto>
    {
        private readonly IGenericRepository<Job> _jobRepository;
        public GetJobByIdHandler(IGenericRepository<Job> jobRepository)
        {
            _jobRepository = jobRepository;
        }
        public async Task<JobResponseDto> Handle(GetJobByIdQuery request, CancellationToken cancellationToken)
        {
            var job = await _jobRepository.GetByIdAsync(request.Id);
            if (job is null) return null;
            return new JobResponseDto
            {
                Id = job.Id,
                Title = job.Title,
                Description = job.Description,
                IsActive = job.IsActive,
                RecruiterId = job.RecruiterId
            };
        }
    }
}
