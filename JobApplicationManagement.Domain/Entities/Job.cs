using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplicationManagement.Domain.Entities
{
    public class Job
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int? RecruiterId { get; set; }
        public Recruiter? Recruiter { get; set; }
        public ICollection<JobCandidateApplication> JobCandidateApplications { get; set; } = new List<JobCandidateApplication>();   
    }
}
