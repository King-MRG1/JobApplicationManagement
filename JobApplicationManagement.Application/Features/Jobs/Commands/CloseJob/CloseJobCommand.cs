using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplicationManagement.Application.Features.Jobs.CloseJob
{
    public class CloseJobCommand : IRequest<int>
    {
        public int JobId { get; set; }
        public int RecruiterId { get; set; }
    }
}
