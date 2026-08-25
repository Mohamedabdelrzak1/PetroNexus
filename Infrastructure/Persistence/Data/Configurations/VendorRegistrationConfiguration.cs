using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Data.Configurations
{
    public class VendorRegistrationConfiguration : IEntityTypeConfiguration<VendorRegistration>
    {
        public void Configure(EntityTypeBuilder<VendorRegistration> builder)
        {
            builder.HasIndex(v => v.RegistrationNumber).IsUnique();

            builder.HasMany(v => v.Documents)
                .WithOne(d => d.VendorRegistration)
                .HasForeignKey(d => d.VendorRegistrationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class RegistrationDocumentConfiguration : IEntityTypeConfiguration<RegistrationDocument>
    {
        public void Configure(EntityTypeBuilder<RegistrationDocument> builder)
        {
            // العلاقة مع VendorRegistration متعرّفة في VendorRegistrationConfiguration
        }
    }
}
