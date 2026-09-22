using JobApplicationManagement.Application.Dtos.CandidateDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplicationManagement.Application.Features.Candidates.Queries.GetCandidates
{
    public class GetCandidatesQuery : IRequest<List<CandidateResponseDto>>
    {
    }
}
