using JobApplicationManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace JobApplicationManagement.Infrastructure.Persistence.Configurations
{
    internal class RecruiterConfiguration : IEntityTypeConfiguration<Recruiter>
    {
        public void Configure(EntityTypeBuilder<Recruiter> builder)
        {
            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(r => r.UserId)
                .IsRequired();
            builder.HasOne(r => r.ApplicationUser)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
