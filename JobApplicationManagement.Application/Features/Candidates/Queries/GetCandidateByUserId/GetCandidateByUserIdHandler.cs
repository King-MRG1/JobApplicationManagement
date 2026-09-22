using JobApplicationManagement.Application.Services;
using JobApplicationManagement.Domain.Entities;
using MediatR;

namespace JobApplicationManagement.Application.Features.Candidates.Queries.GetCandidateByUserId
{
    public class GetCandidateByUserIdHandler : IRequestHandler<GetCandidateByUserIdQuery, Candidate?>
    {
        private readonly CandidateServices _candidateServices;

        public GetCandidateByUserIdHandler(CandidateServices candidateServices)
        {
            _candidateServices = candidateServices;
        }

        public async Task<Candidate?> Handle(GetCandidateByUserIdQuery request, CancellationToken cancellationToken)
        {
            return await _candidateServices.GetByUserIdAsync(request.UserId);
        }
    }
}
