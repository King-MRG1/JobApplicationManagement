using JobApplicationManagement.Application.Dtos.JobApplicationDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplicationManagement.Application.Features.JobCandidateApplications.Queries.GetJobApplications
{
    public class GetJobApplicationsQuery : IRequest<IEnumerable<JobApplicationResponseDto>>
    {
        public int CandidateId { get; set; }
    }
}
