using JobApplicationManagement.Application.Dtos.JobApplicationDto;
using JobApplicationManagement.Application.Services;
using JobApplicationManagement.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
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

        [HttpPost]
        public async Task<IActionResult> CreateApplication([FromBody] CreateJobApplicationDto applicationDto)
        {
            try
            {
                var createdApplication = await _applicationServices.CreateAsync(applicationDto);
                return CreatedAtAction(nameof(CreateApplication), new { id = createdApplication.Id }, createdApplication);
            }
            catch (DomainException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPatch("{id}/cancel")]
        public async Task<IActionResult> CancelApplication(int id)
        {
            try
            {
                var cancelledApplication = await _applicationServices.CancelApplicationAsync(id);
                return Ok(cancelledApplication);
            }
            catch (DomainException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
