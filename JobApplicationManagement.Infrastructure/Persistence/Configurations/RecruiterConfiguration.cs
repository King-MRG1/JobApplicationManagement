using JobApplicationManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplicationManagement.Infrastructure.Persistence.Configurations
{
    public class RecruiterConfiguration : IEntityTypeConfiguration<Recruiter>
    {
        public void Configure(EntityTypeBuilder<Recruiter> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(r => r.UserId)
                .IsRequired();

            builder.HasOne(r => r.ApplicationUser)
                .WithMany()
                .HasForeignKey(r => r.UserId);

            builder.HasMany(r => r.Jobs)
                .WithOne(j => j.Recruiter)
                .HasForeignKey(j => j.RecruiterId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
