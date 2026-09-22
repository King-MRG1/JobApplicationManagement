using JobApplicationManagement.Application.Dtos.JobApplicationDto;
using JobApplicationManagement.Application.Interfaces;
using JobApplicationManagement.Domain.Entities;
using MediatR;


namespace JobApplicationManagement.Application.Features.JobCandidateApplications.Queries.GetJobApplications
{
    public class GetJobApplicationsHandler : IRequestHandler<GetJobApplicationsQuery, IEnumerable<JobApplicationResponseDto>>
    {
        private readonly IGenericRepository<JobCandidateApplication> _jobApplicationRepository;

        public GetJobApplicationsHandler(IGenericRepository<JobCandidateApplication> jobApplicationRepository)
        {
            _jobApplicationRepository = jobApplicationRepository;
        }

        public async Task<IEnumerable<JobApplicationResponseDto>> Handle(GetJobApplicationsQuery request, CancellationToken cancellationToken)
        {
            var jobApplications = await _jobApplicationRepository.GetAsync();
            return jobApplications.Select(j => new JobApplicationResponseDto
            {
                Id = j.Id,
                CandidateId = j.CandidateId,
                JobId = j.JobId,
                AppliedAt = DateTime.UtcNow,
                StatusUpdatedAt = DateTime.UtcNow,
                Status = j.JobApplicationStatus
            });
        }
    }
}
