using Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Data.Configurations
{
    public class LiquidatedDamageConfiguration : IEntityTypeConfiguration<LiquidatedDamage>
    {
        public void Configure(EntityTypeBuilder<LiquidatedDamage> builder)
        {
            // العلاقة مع Shipment متعرّفة في ShipmentConfiguration (Cascade)
        }
    }
}
