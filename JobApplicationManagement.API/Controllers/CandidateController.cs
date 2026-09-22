using JobApplicationManagement.Application.Features.Candidates.Queries.GetCandidateById;
using JobApplicationManagement.Application.Features.Candidates.Queries.GetCandidates;
using JobApplicationManagement.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUserService;
        public CandidateController(IMediator mediator, ICurrentUserService currentUserService)
        {
            _mediator = mediator;
            _currentUserService = currentUserService;
        }
        [HttpGet("candidates")]
        public async Task<IActionResult> GetCandidates()
        {
            var candidates = await _mediator.Send(new GetCandidatesQuery());
            return Ok(candidates);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCandidateById(int id)
        {
            var candidate = await _mediator.Send(new GetCandidateByIdQuery { Id = id });
            if (candidate is null)
                return NotFound();
            return Ok(candidate);
        }
    }
}
