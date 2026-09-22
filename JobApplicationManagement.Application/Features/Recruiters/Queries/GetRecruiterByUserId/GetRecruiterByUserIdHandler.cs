using JobApplicationManagement.Application.Services;
using JobApplicationManagement.Domain.Entities;
using MediatR;

namespace JobApplicationManagement.Application.Features.Recruiters.Queries.GetRecruiterByUserId
{
    public class GetRecruiterByUserIdHandler : IRequestHandler<GetRecruiterByUserIdQuery, Recruiter?>
    {
        private readonly RecruiterServices _recruiterServices;

        public GetRecruiterByUserIdHandler(RecruiterServices recruiterServices)
        {
            _recruiterServices = recruiterServices;
        }

        public async Task<Recruiter?> Handle(GetRecruiterByUserIdQuery request, CancellationToken cancellationToken)
        {
            return await _recruiterServices.GetByUserIdAsync(request.UserId);
        }
    }
}
