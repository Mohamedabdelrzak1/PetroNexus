using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Data.Configurations
{
    public class QualityInspectionConfiguration : IEntityTypeConfiguration<QualityInspection>
    {
        public void Configure(EntityTypeBuilder<QualityInspection> builder)
        {
            builder.HasMany(q => q.NonConformanceReports)
                .WithOne(n => n.QualityInspection)
                .HasForeignKey(n => n.QualityInspectionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class NonConformanceReportConfiguration : IEntityTypeConfiguration<NonConformanceReport>
    {
        public void Configure(EntityTypeBuilder<NonConformanceReport> builder)
        {
            // العلاقة مع QualityInspection متعرّفة في QualityInspectionConfiguration
        }
    }
}
