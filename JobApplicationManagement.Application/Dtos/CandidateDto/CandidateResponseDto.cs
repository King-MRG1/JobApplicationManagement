using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplicationManagement.Application.Dtos.CandidateDto
{
    public class CandidateResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CvUrl { get; set; } = string.Empty;
    }
}
