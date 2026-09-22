using JobApplicationManagement.Application.Dtos.JobApplicationDto;
using MediatR;

namespace JobApplicationManagement.Application.Features.JobCandidateApplications.Commands.CancelJobApplication
{
    public class CancelJobApplicationCommand : IRequest<JobApplicationResponseDto>
    {
        public int ApplicationId { get; set; }
        public int CandidateId { get; set; }
    }
}
