using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Data.Configurations
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasIndex(i => i.InvoiceNumber).IsUnique();

            builder.HasOne(i => i.Tender)
                .WithMany()
                .HasForeignKey(i => i.TenderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.JournalEntry)
                .WithMany()
                .HasForeignKey(i => i.JournalEntryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(i => i.Items)
                .WithOne(ii => ii.Invoice)
                .HasForeignKey(ii => ii.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(i => i.Payments)
                .WithOne(p => p.Invoice)
                .HasForeignKey(p => p.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class InvoiceItemConfiguration : IEntityTypeConfiguration<InvoiceItem>
    {
        public void Configure(EntityTypeBuilder<InvoiceItem> builder)
        {
            // العلاقة مع Invoice متعرّفة في InvoiceConfiguration
        }
    }

    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasOne(p => p.JournalEntry)
                .WithMany()
                .HasForeignKey(p => p.JournalEntryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
