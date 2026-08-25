using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Data.Configurations
{
    public class EscalationLogConfiguration : IEntityTypeConfiguration<EscalationLog>
    {
        public void Configure(EntityTypeBuilder<EscalationLog> builder)
        {
            builder.HasOne(e => e.SentToUser)
                .WithMany()
                .HasForeignKey(e => e.SentToUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
