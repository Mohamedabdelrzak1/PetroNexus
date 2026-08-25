using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Data.Configurations
{
    public class CostCenterConfiguration : IEntityTypeConfiguration<CostCenter>
    {
        public void Configure(EntityTypeBuilder<CostCenter> builder)
        {
            builder.HasIndex(c => c.Code).IsUnique();

            builder.HasOne(c => c.Tender)
                .WithOne(t => t.CostCenter)
                .HasForeignKey<CostCenter>(c => c.TenderId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
