using JobApplicationManagement.Application.Dtos.CandidateDto;
using MediatR;

namespace JobApplicationManagement.Application.Features.Candidates.Queries.GetCandidateById
{
    public class GetCandidateByIdQuery : IRequest<CandidateResponseDto?>
    {
        public int Id { get; set; }
    }
}
