using JobApplicationManagement.Domain.Entities;
using MediatR;

namespace JobApplicationManagement.Application.Features.Candidates.Queries.GetCandidateByUserId
{
    public class GetCandidateByUserIdQuery : IRequest<Candidate?>
    {
        public string UserId { get; set; } = null!;
    }
}
