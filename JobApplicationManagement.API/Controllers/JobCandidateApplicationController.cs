using JobApplicationManagement.Application.Dtos.JobApplicationDto;
using JobApplicationManagement.Application.Services;
using JobApplicationManagement.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
namespace JobApplicationManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobCandidateApplicationController : ControllerBase
    {
        private readonly JobCandidateApplicationServices _applicationServices;
        public JobCandidateApplicationController(JobCandidateApplicationServices applicationServices)
        {
            _applicationServices = applicationServices;
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
        public async Task<IActionResult> CreateApplication([FromBody] CreateJobApplicationDto dto)
        {
            try
            {
                var application = await _applicationServices.CreateAsync(dto);
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
        public async Task<IActionResult> CancelApplication(int id)
        {
            try
            {
                var application = await _applicationServices.CancelApplicationAsync(id);
                return Ok(application);
            }
            catch (DomainException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}