using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Data.Configurations
{
    public class LetterOfCreditConfiguration : IEntityTypeConfiguration<LetterOfCredit>
    {
        public void Configure(EntityTypeBuilder<LetterOfCredit> builder)
        {
            builder.HasIndex(l => l.LcNumber).IsUnique();

            builder.HasOne(l => l.JournalEntry)
                .WithMany()
                .HasForeignKey(l => l.JournalEntryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(l => l.PurchaseOrders)
                .WithOne(p => p.LetterOfCredit)
                .HasForeignKey(p => p.LetterOfCreditId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
