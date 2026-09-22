using JobApplicationManagement.Application.Dtos.CandidateDto;
using MediatR;

namespace JobApplicationManagement.Application.Features.Candidates.Commands.CreateCandidate
{
    public class CreateCandidateCommand : IRequest<CandidateResponseDto>
    {
        public string Name { get; set; } = null!;
        public string CvUrl { get; set; } = null!;
        public string UserId { get; set; } = null!;
    }
}
