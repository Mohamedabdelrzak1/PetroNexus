using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Data.Configurations
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasOne(n => n.TargetUser)
                .WithMany()
                .HasForeignKey(n => n.TargetUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
