using JobApplicationManagement.Application.Dtos.JobApplicationDto;
using MediatR;

namespace JobApplicationManagement.Application.Features.JobCandidateApplications.Queries.GetJobApplicationById
{
    public class GetJobApplicationByIdQuery : IRequest<JobApplicationResponseDto?>
    {
        public int Id { get; set; }
    }
}
