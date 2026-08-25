using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Data.Configurations
{
    public class PrincipalPerformanceReviewConfiguration : IEntityTypeConfiguration<PrincipalPerformanceReview>
    {
        public void Configure(EntityTypeBuilder<PrincipalPerformanceReview> builder)
        {
            // العلاقة مع Principal متعرّفة في PrincipalConfiguration (Cascade)

            builder.HasOne(r => r.ReviewedByUser)
                .WithMany()
                .HasForeignKey(r => r.ReviewedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
