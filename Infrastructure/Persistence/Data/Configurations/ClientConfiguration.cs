using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Data.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasMany(c => c.Contacts)
                .WithOne(cc => cc.Client)
                .HasForeignKey(cc => cc.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Tenders)
                .WithOne(t => t.Client)
                .HasForeignKey(t => t.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Invoices)
                .WithOne(i => i.Client)
                .HasForeignKey(i => i.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Registrations)
                .WithOne(r => r.Client)
                .HasForeignKey(r => r.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Interactions)
                .WithOne(i => i.Client)
                .HasForeignKey(i => i.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.PortalUsers)
                .WithOne(p => p.Client)
                .HasForeignKey(p => p.ClientId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class ClientContactConfiguration : IEntityTypeConfiguration<ClientContact>
    {
        public void Configure(EntityTypeBuilder<ClientContact> builder)
        {
            // العلاقة مع Client متعرّفة في ClientConfiguration
        }
    }
}
