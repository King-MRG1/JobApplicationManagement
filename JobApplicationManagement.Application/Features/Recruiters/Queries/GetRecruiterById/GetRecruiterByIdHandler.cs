using JobApplicationManagement.Application.Services;
using JobApplicationManagement.Domain.Entities;
using MediatR;

namespace JobApplicationManagement.Application.Features.Recruiters.Queries.GetRecruiterById
{
    public class GetRecruiterByIdHandler : IRequestHandler<GetRecruiterByIdQuery, Recruiter?>
    {
        private readonly RecruiterServices _recruiterServices;

        public GetRecruiterByIdHandler(RecruiterServices recruiterServices)
        {
            _recruiterServices = recruiterServices;
        }

        public async Task<Recruiter?> Handle(GetRecruiterByIdQuery request, CancellationToken cancellationToken)
        {
            return await _recruiterServices.GetByIdAsync(request.Id);
        }
    }
}
