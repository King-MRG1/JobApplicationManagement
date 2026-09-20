using JobApplicationManagement.Application.Dtos.JobApplicationDto;
using JobApplicationManagement.Application.Interfaces;
using JobApplicationManagement.Application.Services;
using JobApplicationManagement.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace JobApplicationManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobCandidateApplicationController : ControllerBase
    {
        private readonly JobCandidateApplicationServices _applicationServices;
        private readonly ICurrentUserService _currentUserService;
        public JobCandidateApplicationController(JobCandidateApplicationServices applicationServices, ICurrentUserService currentUserService)
        {
            _applicationServices = applicationServices;
            _currentUserService = currentUserService;
        }
        /// <summary>GET /api/jobcandidateapplication/{id}</summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetApplicationById(int id)
        {
            var application = await _applicationServices.GetByIdAsync(id);
            if (application is null)
                return NotFound();
            return Ok(application);
        }
        /// <summary>POST /api/jobcandidateapplication — submits a new job application.</summary>
        [HttpPost]
        [Authorize(Roles = "Candidate")]
        public async Task<IActionResult> CreateApplication([FromBody] CreateJobApplicationDto dto)
        {
            try
            {
                var candidateId = _currentUserService.CandidateId;
                if (candidateId is null)
                {
                    return Forbid();
                }
                var application = await _applicationServices.CreateAsync(dto, candidateId.Value);
                return CreatedAtAction(nameof(GetApplicationById), new { id = application.Id }, application);
            }
            catch (DomainException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        /// <summary>
        /// PATCH /api/jobcandidateapplication/{id}/cancel — cancels an application.
        /// Only allowed while status is Applied or UnderReview.
        /// </summary>
        [HttpPatch("{id}/cancel")]
        [Authorize(Roles = "Candidate")]
        public async Task<IActionResult> CancelApplication(int id)
        {
            try
            {
                var candidateId = _currentUserService.CandidateId;
                if (candidateId is null)
                {
                    return Forbid();
                }
                var application = await _applicationServices.CancelApplicationAsync(id, candidateId.Value);
                return Ok(application);
            }
            catch (DomainException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}