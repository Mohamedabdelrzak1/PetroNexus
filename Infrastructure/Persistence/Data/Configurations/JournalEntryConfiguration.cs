using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Data.Configurations
{
    public class JournalEntryConfiguration : IEntityTypeConfiguration<JournalEntry>
    {
        public void Configure(EntityTypeBuilder<JournalEntry> builder)
        {
            builder.HasMany(j => j.Lines)
                .WithOne(l => l.JournalEntry)
                .HasForeignKey(l => l.JournalEntryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class JournalEntryLineConfiguration : IEntityTypeConfiguration<JournalEntryLine>
    {
        public void Configure(EntityTypeBuilder<JournalEntryLine> builder)
        {
            builder.HasOne(l => l.Account)
                .WithMany()
                .HasForeignKey(l => l.AccountId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(l => l.CostCenter)
                .WithMany(c => c.JournalEntryLines)
                .HasForeignKey(l => l.CostCenterId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
