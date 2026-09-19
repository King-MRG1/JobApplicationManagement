using JobApplicationManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplicationManagement.Infrastructure.Persistence.Configurations
{
    internal class JobCandidateApplicationConfiguration : IEntityTypeConfiguration<JobCandidateApplication>
    {
        public void Configure(EntityTypeBuilder<JobCandidateApplication> builder)
        {
            builder.Property(jca => jca.JobApplicationStatus)
                .IsRequired();

            builder.Property(jca => jca.AppliedAt)
                .IsRequired();

            builder.Property(jca => jca.StatusUpdatedAt)
                .IsRequired();

            builder.HasOne(jca => jca.Candidate)
                .WithMany(c => c.JobCandidateApplications)
                .HasForeignKey(jca => jca.CandidateId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(jca => jca.Job)
                .WithMany(j => j.JobCandidateApplications)
                .HasForeignKey(jca => jca.JobId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
