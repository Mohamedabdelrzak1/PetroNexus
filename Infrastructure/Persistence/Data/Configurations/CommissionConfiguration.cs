using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Data.Configurations
{
    public class CommissionConfiguration : IEntityTypeConfiguration<Commission>
    {
        public void Configure(EntityTypeBuilder<Commission> builder)
        {
            // العلاقة مع Principal متعرّفة في PrincipalConfiguration (Restrict)

            builder.HasOne(c => c.PurchaseOrder)
                .WithMany(p => p.Commissions)
                .HasForeignKey(c => c.PurchaseOrderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.JournalEntry)
                .WithMany()
                .HasForeignKey(c => c.JournalEntryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
