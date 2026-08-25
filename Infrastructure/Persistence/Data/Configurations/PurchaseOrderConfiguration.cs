using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Data.Configurations
{
    public class PurchaseOrderConfiguration : IEntityTypeConfiguration<PurchaseOrder>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrder> builder)
        {
            builder.HasIndex(p => p.PoNumber).IsUnique();

            builder.HasOne(p => p.Principal)
                .WithMany()
                .HasForeignKey(p => p.PrincipalId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Tender)
                .WithMany()
                .HasForeignKey(p => p.TenderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.JournalEntry)
                .WithMany()
                .HasForeignKey(p => p.JournalEntryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.Items)
                .WithOne(i => i.PurchaseOrder)
                .HasForeignKey(i => i.PurchaseOrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Shipments)
                .WithOne(s => s.PurchaseOrder)
                .HasForeignKey(s => s.PurchaseOrderId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class PurchaseOrderItemConfiguration : IEntityTypeConfiguration<PurchaseOrderItem>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrderItem> builder)
        {
            builder.HasOne(i => i.PrincipalProduct)
                .WithMany()
                .HasForeignKey(i => i.PrincipalProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
