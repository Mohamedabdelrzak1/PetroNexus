using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Data.Configurations
{
    public class TenderConfiguration : IEntityTypeConfiguration<Tender>
    {
        public void Configure(EntityTypeBuilder<Tender> builder)
        {
            builder.HasMany(t => t.Items)
                .WithOne(i => i.Tender)
                .HasForeignKey(i => i.TenderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.TenderLead)
                .WithMany()
                .HasForeignKey(t => t.TenderLeadId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class TenderItemConfiguration : IEntityTypeConfiguration<TenderItem>
    {
        public void Configure(EntityTypeBuilder<TenderItem> builder)
        {
            builder.HasOne(i => i.PrincipalProduct)
                .WithMany()
                .HasForeignKey(i => i.PrincipalProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
