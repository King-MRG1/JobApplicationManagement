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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCandidateById(int id)
        {
            var candidate = await _candidateServices.GetByIdAsync(id);
            if (candidate == null)
            {
                return NotFound();
            }
            return Ok(candidate);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCandidate([FromBody] CreateCandidateDto candidateDto)
        {
            var createdCandidate = await _candidateServices.CreateAsync(candidateDto);
            return CreatedAtAction(nameof(GetCandidateById), new { id = createdCandidate.Id }, createdCandidate);
        }
    }
}
