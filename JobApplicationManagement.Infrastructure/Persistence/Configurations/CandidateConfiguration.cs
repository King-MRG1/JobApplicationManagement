using JobApplicationManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace JobApplicationManagement.Infrastructure.Persistence.Configurations
{
    internal class CandidateConfiguration : IEntityTypeConfiguration<Candidate>
    {
        public void Configure(EntityTypeBuilder<Candidate> builder)
        {
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(c => c.CvUrl)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(c => c.UserId)
                .IsRequired();
            builder.HasOne(c => c.ApplicationUser)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}