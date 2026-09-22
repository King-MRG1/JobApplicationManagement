using JobApplicationManagement.Application.Dtos.CandidateDto;
using JobApplicationManagement.Application.Services;
using MediatR;

namespace JobApplicationManagement.Application.Features.Candidates.Queries.GetCandidateById
{
    public class GetCandidateByIdHandler : IRequestHandler<GetCandidateByIdQuery, CandidateResponseDto?>
    {
        private readonly CandidateServices _candidateServices;

        public GetCandidateByIdHandler(CandidateServices candidateServices)
        {
            _candidateServices = candidateServices;
        }

        public async Task<CandidateResponseDto?> Handle(GetCandidateByIdQuery request, CancellationToken cancellationToken)
        {
            return await _candidateServices.GetByIdAsync(request.Id);
        }
    }
}
