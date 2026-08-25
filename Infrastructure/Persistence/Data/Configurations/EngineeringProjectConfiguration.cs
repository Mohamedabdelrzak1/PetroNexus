using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Data.Configurations
{
    public class EngineeringProjectConfiguration : IEntityTypeConfiguration<EngineeringProject>
    {
        public void Configure(EntityTypeBuilder<EngineeringProject> builder)
        {
            builder.HasOne(e => e.Tender)
                .WithMany(t => t.EngineeringProjects)
                .HasForeignKey(e => e.TenderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.Deliverables)
                .WithOne(d => d.EngineeringProject)
                .HasForeignKey(d => d.EngineeringProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class EngineeringDeliverableConfiguration : IEntityTypeConfiguration<EngineeringDeliverable>
    {
        public void Configure(EntityTypeBuilder<EngineeringDeliverable> builder)
        {
            // العلاقة مع EngineeringProject متعرّفة في EngineeringProjectConfiguration
        }
    }
}
