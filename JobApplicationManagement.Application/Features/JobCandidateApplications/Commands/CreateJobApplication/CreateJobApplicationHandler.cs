using JobApplicationManagement.Application.Dtos.JobApplicationDto;
using JobApplicationManagement.Application.Services;
using MediatR;

namespace JobApplicationManagement.Application.Features.JobCandidateApplications.Commands.CreateJobApplication
{
    public class CreateJobApplicationHandler : IRequestHandler<CreateJobApplicationCommand, JobApplicationResponseDto>
    {
        private readonly JobCandidateApplicationServices _applicationServices;

        public CreateJobApplicationHandler(JobCandidateApplicationServices applicationServices)
        {
            _applicationServices = applicationServices;
        }

        public async Task<JobApplicationResponseDto> Handle(CreateJobApplicationCommand request, CancellationToken cancellationToken)
        {
            var createDto = new CreateJobApplicationDto
            {
                JobId = request.JobId
            };

            return await _applicationServices.CreateAsync(createDto, request.CandidateId);
        }
    }
}
