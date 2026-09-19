using JobApplicationManagement.Application.Dtos.JobDto;
using JobApplicationManagement.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly JobServices _jobServices;
        public JobController(JobServices jobServices)
        {
            _jobServices = jobServices;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobById(int id)
        {
            var job = await _jobServices.GetByIdAsync(id);
            if (job == null)
            {
                return NotFound();
            }
            return Ok(job);
        }

        [HttpPost]
        public async Task<IActionResult> CreateJob([FromBody] CreateJobDto jobDto)
        {
            var createdJob = await _jobServices.CreateAsync(jobDto);
            return CreatedAtAction(nameof(GetJobById), new { id = createdJob.Id }, createdJob);
        }
    }
}
