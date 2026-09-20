using JobApplicationManagement.Application.Dtos.CandidateDto;
using JobApplicationManagement.Application.Interfaces;
using JobApplicationManagement.Application.Services;
using JobApplicationManagement.Infrastructure.Migrations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly CandidateServices _candidateServices;
        private readonly ICurrentUserService _currentUserService;
        public CandidateController(CandidateServices candidateServices, ICurrentUserService currentUserService)
        {
            _candidateServices = candidateServices;
            _currentUserService = currentUserService;
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
        //[HttpPost]
        //[Authorize]
        //public async Task<IActionResult> CreateCandidate([FromBody] CreateCandidateDto dto)
        //{
        //    var CandidateId =  _currentUserService.CandidateId;

        //    if(CandidateId is null)
        //    {
        //        return Forbid();
        //    }
        //    var candidate = await _candidateServices.CreateAsync(dto, CandidateId);
        //    return CreatedAtAction(nameof(GetCandidateById), new { id = candidate.Id }, candidate);
        //}
    }
}
