using JobApplicationManagement.Domain.Entities;
using MediatR;

namespace JobApplicationManagement.Application.Features.Recruiters.Commands.CreateRecruiter
{
    public class CreateRecruiterCommand : IRequest<Recruiter>
    {
        public string Name { get; set; } = null!;
        public string UserId { get; set; } = null!;
    }
}
