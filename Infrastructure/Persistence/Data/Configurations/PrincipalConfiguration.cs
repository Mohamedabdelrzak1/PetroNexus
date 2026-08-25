using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Data.Configurations
{
    public class PrincipalConfiguration : IEntityTypeConfiguration<Principal>
    {
        public void Configure(EntityTypeBuilder<Principal> builder)
        {
            builder.HasMany(p => p.Products)
                .WithOne(pp => pp.Principal)
                .HasForeignKey(pp => pp.PrincipalId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Contacts)
                .WithOne(pc => pc.Principal)
                .HasForeignKey(pc => pc.PrincipalId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.PerformanceReviews)
                .WithOne(r => r.Principal)
                .HasForeignKey(r => r.PrincipalId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Commissions)
                .WithOne(c => c.Principal)
                .HasForeignKey(c => c.PrincipalId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class PrincipalProductConfiguration : IEntityTypeConfiguration<PrincipalProduct>
    {
        public void Configure(EntityTypeBuilder<PrincipalProduct> builder)
        {
            // العلاقة مع Principal متعرّفة في PrincipalConfiguration
        }
    }
}
