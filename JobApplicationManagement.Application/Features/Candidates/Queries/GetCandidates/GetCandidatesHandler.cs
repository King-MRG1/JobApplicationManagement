using JobApplicationManagement.Application.Dtos.CandidateDto;
using JobApplicationManagement.Application.Interfaces;
using JobApplicationManagement.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplicationManagement.Application.Features.Candidates.Queries.GetCandidates
{
    public class GetCandidatesHandler : IRequestHandler<GetCandidatesQuery, List<CandidateResponseDto>>
    {
        private readonly IGenericRepository<Candidate> _candidateRepository;

        public GetCandidatesHandler(IGenericRepository<Candidate> candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        async Task<List<CandidateResponseDto>> IRequestHandler<GetCandidatesQuery, List<CandidateResponseDto>>.Handle(GetCandidatesQuery request, CancellationToken cancellationToken)
        {
            var candidates = await _candidateRepository.GetAsync();
            return candidates.Select(c => new CandidateResponseDto
            {
                Id = c.Id,
                Name = c.Name,
                CvUrl = c.CvUrl
            }).ToList();
        }
    }
}
