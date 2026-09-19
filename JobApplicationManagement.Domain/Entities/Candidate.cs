using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplicationManagement.Domain
{
    public class Candidate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CvUrl { get; set; }
       public IEnumerable<JobCandidateApplication> JobCandidateApplications { get; set; } = Enumerable.Empty<JobCandidateApplication>();
    }
}
