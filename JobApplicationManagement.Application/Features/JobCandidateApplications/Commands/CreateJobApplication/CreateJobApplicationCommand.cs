using JobApplicationManagement.Application.Dtos.JobApplicationDto;
using MediatR;

namespace JobApplicationManagement.Application.Features.JobCandidateApplications.Commands.CreateJobApplication
{
    public class CreateJobApplicationCommand : IRequest<JobApplicationResponseDto>
    {
        public int JobId { get; set; }
        public int CandidateId { get; set; }
    }
}
