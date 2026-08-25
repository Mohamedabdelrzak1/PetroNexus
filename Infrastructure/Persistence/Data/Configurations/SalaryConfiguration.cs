using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Data.Configurations
{
    public class SalaryConfiguration : IEntityTypeConfiguration<Salary>
    {
        public void Configure(EntityTypeBuilder<Salary> builder)
        {
            // العلاقة مع Employee متعرّفة في EmployeeConfiguration (HasMany Cascade)

            builder.HasOne(s => s.JournalEntry)
                .WithMany()
                .HasForeignKey(s => s.JournalEntryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
