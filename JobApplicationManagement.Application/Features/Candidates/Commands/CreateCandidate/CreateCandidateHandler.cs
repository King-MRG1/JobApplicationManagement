using JobApplicationManagement.Application.Dtos.CandidateDto;
using JobApplicationManagement.Application.Interfaces;
using JobApplicationManagement.Application.Services;
using JobApplicationManagement.Domain.Entities;
using MediatR;

namespace JobApplicationManagement.Application.Features.Candidates.Commands.CreateCandidate
{
    public class CreateCandidateHandler : IRequestHandler<CreateCandidateCommand, CandidateResponseDto>
    {
        private readonly CandidateServices _candidateServices;

        public CreateCandidateHandler(CandidateServices candidateServices)
        {
            _candidateServices = candidateServices;
        }

        public async Task<CandidateResponseDto> Handle(CreateCandidateCommand request, CancellationToken cancellationToken)
        {
            var createDto = new CreateCandidateDto
            {
                Name = request.Name,
                CvUrl = request.CvUrl
            };

            var result = await _candidateServices.CreateAsync(createDto, request.UserId);
            return result;
        }
    }
}
