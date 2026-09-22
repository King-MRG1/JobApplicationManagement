using JobApplicationManagement.Application.Dtos.JobDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplicationManagement.Application.Features.Jobs.Queries.GetAllJob
{
    public class GetAllJobQuery : IRequest<List<JobResponseDto>>
    {

    }
}
