using JobApplicationManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace JobApplicationManagement.Infrastructure.Persistence.Configurations
{
    internal class JobConfiguration : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder.Property(j => j.Title)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(j => j.Description)
                .IsRequired()
                .HasMaxLength(1000);
            builder.HasOne(j => j.Recruiter)
                .WithMany(r => r.Jobs)
                .HasForeignKey(j => j.RecruiterId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}