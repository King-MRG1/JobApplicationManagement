using JobApplicationManagement.Application.Dtos.JobApplicationDto;
using JobApplicationManagement.Application.Services;
using MediatR;

namespace JobApplicationManagement.Application.Features.JobCandidateApplications.Commands.CancelJobApplication
{
    public class CancelJobApplicationHandler : IRequestHandler<CancelJobApplicationCommand, JobApplicationResponseDto>
    {
        private readonly JobCandidateApplicationServices _applicationServices;

        public CancelJobApplicationHandler(JobCandidateApplicationServices applicationServices)
        {
            _applicationServices = applicationServices;
        }

        public async Task<JobApplicationResponseDto> Handle(CancelJobApplicationCommand request, CancellationToken cancellationToken)
        {
            return await _applicationServices.CancelApplicationAsync(request.ApplicationId, request.CandidateId);
        }
    }
}
