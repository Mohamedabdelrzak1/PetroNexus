using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Data.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasIndex(a => a.Code).IsUnique();

            builder.HasOne(a => a.ParentAccount)
                .WithMany()
                .HasForeignKey(a => a.ParentAccountId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
