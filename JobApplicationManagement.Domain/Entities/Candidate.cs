using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplicationManagement.Domain.Entities
{
    public class Candidate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CvUrl { get; set; }
        public ICollection<JobCandidateApplication> JobCandidateApplications { get; set; } = new List<JobCandidateApplication>();
    }
}
