using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplicationManagement.Domain
{
    public class Job
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; } 
        public IEnumerable<JobCandidateApplication> JobCandidateApplications { get; set; } = Enumerable.Empty<JobCandidateApplication>();
    }
}
