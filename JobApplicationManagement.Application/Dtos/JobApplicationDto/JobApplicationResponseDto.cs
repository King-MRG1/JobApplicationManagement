using JobApplicationManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplicationManagement.Application.Dtos.JobApplicationDto
{
    public class JobApplicationResponseDto
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public int JobId { get; set; }
        public JobApplicationStatus Status { get; set; }
        public DateTime AppliedAt { get; set; }
        public DateTime StatusUpdatedAt { get; set; }
    }
}
