using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Data.Configurations
{
    public class QuotationConfiguration : IEntityTypeConfiguration<Quotation>
    {
        public void Configure(EntityTypeBuilder<Quotation> builder)
        {
            builder.HasOne(q => q.Tender)
                .WithMany(t => t.Quotations)
                .HasForeignKey(q => q.TenderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(q => q.Items)
                .WithOne(i => i.Quotation)
                .HasForeignKey(i => i.QuotationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class QuotationItemConfiguration : IEntityTypeConfiguration<QuotationItem>
    {
        public void Configure(EntityTypeBuilder<QuotationItem> builder)
        {
            builder.HasOne(i => i.PrincipalProduct)
                .WithMany()
                .HasForeignKey(i => i.PrincipalProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
