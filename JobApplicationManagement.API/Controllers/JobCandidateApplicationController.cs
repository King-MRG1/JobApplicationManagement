using JobApplicationManagement.Application.Dtos.JobApplicationDto;
using JobApplicationManagement.Application.Features.JobCandidateApplications.Commands.CancelJobApplication;
using JobApplicationManagement.Application.Features.JobCandidateApplications.Commands.CreateJobApplication;
using JobApplicationManagement.Application.Features.JobCandidateApplications.Queries.GetJobApplicationById;
using JobApplicationManagement.Application.Features.JobCandidateApplications.Queries.GetJobApplications;
using JobApplicationManagement.Application.Interfaces;
using JobApplicationManagement.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace JobApplicationManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobCandidateApplicationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUserService;
        public JobCandidateApplicationController(IMediator mediator, ICurrentUserService currentUserService)
        {
            _mediator = mediator;
            _currentUserService = currentUserService;
        }
        [HttpGet("myApplications")]
        public async Task<IActionResult> GetApplications()
        {
            var candidateId = _currentUserService.CandidateId;
            if (candidateId is null)
            {
                return Forbid();
            }
            var applications = await _mediator.Send(new GetJobApplicationsQuery { CandidateId = candidateId.Value });
            return Ok(applications);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetApplicationById(int id)
        {
            var application = await _mediator.Send(new GetJobApplicationByIdQuery { Id = id });
            if (application is null)
                return NotFound();
            return Ok(application);
        }
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
                var application = await _mediator.Send(new CreateJobApplicationCommand
                {
                    JobId = dto.JobId,
                    CandidateId = candidateId.Value
                });
                return CreatedAtAction(nameof(GetApplicationById), new { id = application.Id }, application);
            }
            catch (DomainException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
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
                var application = await _mediator.Send(new CancelJobApplicationCommand
                {
                    ApplicationId = id,
                    CandidateId = candidateId.Value
                });
                return Ok(application);
            }
            catch (DomainException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}