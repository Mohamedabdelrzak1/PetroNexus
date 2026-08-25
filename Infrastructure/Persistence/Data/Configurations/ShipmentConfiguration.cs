using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Data.Configurations
{
    public class ShipmentConfiguration : IEntityTypeConfiguration<Shipment>
    {
        public void Configure(EntityTypeBuilder<Shipment> builder)
        {
            builder.HasOne(s => s.JournalEntry)
                .WithMany()
                .HasForeignKey(s => s.JournalEntryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(s => s.TrackingEvents)
                .WithOne(e => e.Shipment)
                .HasForeignKey(e => e.ShipmentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(s => s.LiquidatedDamages)
                .WithOne(l => l.Shipment)
                .HasForeignKey(l => l.ShipmentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class ShipmentTrackingEventConfiguration : IEntityTypeConfiguration<ShipmentTrackingEvent>
    {
        public void Configure(EntityTypeBuilder<ShipmentTrackingEvent> builder)
        {
            // العلاقة مع Shipment متعرّفة في ShipmentConfiguration
        }
    }
}
