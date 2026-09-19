using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplicationManagement.Domain.Entities
{
    public class Recruiter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<Job> Jobs { get; set; } = new List<Job>();
    }
}
