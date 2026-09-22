using JobApplicationManagement.Application.Dtos.JobApplicationDto;
using JobApplicationManagement.Application.Services;
using MediatR;

namespace JobApplicationManagement.Application.Features.JobCandidateApplications.Queries.GetJobApplicationById
{
    public class GetJobApplicationByIdHandler : IRequestHandler<GetJobApplicationByIdQuery, JobApplicationResponseDto?>
    {
        private readonly JobCandidateApplicationServices _applicationServices;

        public GetJobApplicationByIdHandler(JobCandidateApplicationServices applicationServices)
        {
            _applicationServices = applicationServices;
        }

        public async Task<JobApplicationResponseDto?> Handle(GetJobApplicationByIdQuery request, CancellationToken cancellationToken)
        {
            return await _applicationServices.GetByIdAsync(request.Id);
        }
    }
}
