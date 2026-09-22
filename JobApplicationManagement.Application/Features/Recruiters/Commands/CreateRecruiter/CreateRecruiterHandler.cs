using JobApplicationManagement.Application.Services;
using JobApplicationManagement.Domain.Entities;
using MediatR;

namespace JobApplicationManagement.Application.Features.Recruiters.Commands.CreateRecruiter
{
    public class CreateRecruiterHandler : IRequestHandler<CreateRecruiterCommand, Recruiter>
    {
        private readonly RecruiterServices _recruiterServices;

        public CreateRecruiterHandler(RecruiterServices recruiterServices)
        {
            _recruiterServices = recruiterServices;
        }

        public async Task<Recruiter> Handle(CreateRecruiterCommand request, CancellationToken cancellationToken)
        {
            return await _recruiterServices.CreateAsync(request.Name, request.UserId);
        }
    }
}
