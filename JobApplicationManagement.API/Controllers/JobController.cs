using JobApplicationManagement.Application;
using JobApplicationManagement.Application.Dtos.JobDto;
using JobApplicationManagement.Application.Interfaces;
using JobApplicationManagement.Application.Services;
using JobApplicationManagement.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly JobServices _jobServices;
        private readonly ICurrentUserService _currentUserService;
        public JobController(JobServices jobServices, ICurrentUserService currentUserService)
        {
            _jobServices = jobServices;
            _currentUserService = currentUserService;
        }
        /// <summary>GET /api/job/{id}</summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobById(int id)
        {
            var job = await _jobServices.GetByIdAsync(id);
            if (job is null)
                return NotFound();
            return Ok(job);
        }
        /// <summary>POST /api/job — creates a new job. Requires Recruiter role.</summary>
        [HttpPost]
        [Authorize(Roles = "Recruiter")]
        public async Task<IActionResult> CreateJob([FromBody] CreateJobDto jobDto)
        {
            var recruiterId = _currentUserService.RecruiterId;
            var job = await _jobServices.CreateAsync(jobDto, recruiterId.Value);
            return CreatedAtAction(nameof(GetJobById), new { id = job.Id }, job);
        }
        /// <summary>
        /// PATCH /api/job/{id}/close — deactivates a job.
        /// The recruiter id is taken from the authenticated user's JWT claims.
        /// Requires Recruiter role.
        /// </summary>
        [HttpPatch("{id}/close")]
        [Authorize(Roles = "Recruiter")]
        public async Task<IActionResult> CloseJob(int id)
        {
            var recruiterId = _currentUserService.RecruiterId;
            if (recruiterId is null)
                return Forbid();
            try
            {
                await _jobServices.CloseJobAsync(id, recruiterId.Value);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}