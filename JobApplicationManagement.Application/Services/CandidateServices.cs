using JobApplicationManagement.Application.Dtos.CandidateDto;
using JobApplicationManagement.Application.Interfaces;
using JobApplicationManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplicationManagement.Application.Services
{
    public class CandidateServices
    {
        private readonly IGenericRepository<Candidate> _candidateRepository;

        public CandidateServices(IGenericRepository<Candidate> candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public async Task<CandidateResponseDto?> GetByIdAsync(int id)
        {
            var candidate = await _candidateRepository.GetByIdAsync(id);
            if (candidate == null)
            {
                return null;
            }

            return MapToResponseDto(candidate);
        }

        public async Task<CandidateResponseDto> CreateAsync(CreateCandidateDto createCandidateDto)
        {
            var candidate = new Candidate()
            {
                Name = createCandidateDto.Name,
                CvUrl = createCandidateDto.CvUrl
            };

            await _candidateRepository.AddAsync(candidate);
            await _candidateRepository.SaveChangesAsync();

            return MapToResponseDto(candidate);
        }

        private CandidateResponseDto MapToResponseDto(Candidate candidate)
        {
            return new CandidateResponseDto
            {
                Id = candidate.Id,
                Name = candidate.Name,
                CvUrl = candidate.CvUrl
            };
        }
    }
}
