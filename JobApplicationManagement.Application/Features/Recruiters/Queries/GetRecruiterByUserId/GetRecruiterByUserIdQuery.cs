using JobApplicationManagement.Domain.Entities;
using MediatR;

namespace JobApplicationManagement.Application.Features.Recruiters.Queries.GetRecruiterByUserId
{
    public class GetRecruiterByUserIdQuery : IRequest<Recruiter?>
    {
        public string UserId { get; set; } = null!;
    }
}
