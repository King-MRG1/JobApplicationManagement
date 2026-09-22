using JobApplicationManagement.Application.Dtos.JobDto;
using JobApplicationManagement.Application.Features.Jobs.CloseJob;
using JobApplicationManagement.Application.Features.Jobs.CreateJob;
using JobApplicationManagement.Application.Features.Jobs.Queries.GetJobById;
using JobApplicationManagement.Application.Features.Jobs.Queries.GetAllJob;
using JobApplicationManagement.Application.Interfaces;
using JobApplicationManagement.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUserService;
        public JobController( ICurrentUserService currentUserService, IMediator mediator)
        {
            _currentUserService = currentUserService;
            _mediator = mediator;
        }

        [HttpGet("Jobs")]
        public async Task<IActionResult> GetJobs()
        {
            var jobs = await _mediator.Send(new GetAllJobQuery());
            return Ok(jobs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobById(int id)
        {
            var job = await _mediator.Send(new GetJobByIdQuery { Id = id });
            if (job is null)
                return NotFound();
            return Ok(job);
        }
        [HttpPost]
        [Authorize(Roles = "Recruiter")]
        public async Task<IActionResult> CreateJob([FromBody] CreateJobDto jobDto)
        {
            var recruiterId = _currentUserService.RecruiterId;
            if (recruiterId is null)
                return Forbid();

            var id = await _mediator.Send(new CreateJobCommand
            {
                Title = jobDto.Title,
                Description = jobDto.Description,
                recruiterId = recruiterId.Value
            });

            return CreatedAtAction(nameof(GetJobById), new { id = id }, null);
        }

        [HttpPatch("{id}/close")]
        [Authorize(Roles = "Recruiter")]
        public async Task<IActionResult> CloseJob(int id)
        {
            var recruiterId = _currentUserService.RecruiterId;
            if (recruiterId is null)
                return Forbid();
            try
            {
                await _mediator.Send(new CloseJobCommand
                {
                    JobId = id,
                    RecruiterId = recruiterId.Value
                });
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}