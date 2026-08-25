using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Data.Configurations
{
    public class FabricationOrderConfiguration : IEntityTypeConfiguration<FabricationOrder>
    {
        public void Configure(EntityTypeBuilder<FabricationOrder> builder)
        {
            builder.HasOne(f => f.PurchaseOrder)
                .WithOne(p => p.FabricationOrder)
                .HasForeignKey<FabricationOrder>(f => f.PurchaseOrderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(f => f.JournalEntry)
                .WithMany()
                .HasForeignKey(f => f.JournalEntryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(f => f.Inspections)
                .WithOne(i => i.FabricationOrder)
                .HasForeignKey(i => i.FabricationOrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
