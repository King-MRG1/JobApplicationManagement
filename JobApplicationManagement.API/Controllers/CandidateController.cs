using JobApplicationManagement.Application.Dtos.CandidateDto;
using JobApplicationManagement.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly CandidateServices _candidateServices;
        public CandidateController(CandidateServices candidateServices)
        {
            _candidateServices = candidateServices;
        }
        /// <summary>GET /api/candidate/{id}</summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCandidateById(int id)
        {
            var candidate = await _candidateServices.GetByIdAsync(id);
            if (candidate is null)
                return NotFound();
            return Ok(candidate);
        }
        /// <summary>POST /api/candidate — creates a new candidate record.</summary>
        [HttpPost]
        public async Task<IActionResult> CreateCandidate([FromBody] CreateCandidateDto dto)
        {
            var candidate = await _candidateServices.CreateAsync(dto);
            return CreatedAtAction(nameof(GetCandidateById), new { id = candidate.Id }, candidate);
        }
    }
}
