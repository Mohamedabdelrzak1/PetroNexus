using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Data.Configurations
{
    public class TenderLeadConfiguration : IEntityTypeConfiguration<TenderLead>
    {
        public void Configure(EntityTypeBuilder<TenderLead> builder)
        {
            builder.HasOne(l => l.ConvertedTender)
                .WithMany()
                .HasForeignKey(l => l.ConvertedTenderId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
