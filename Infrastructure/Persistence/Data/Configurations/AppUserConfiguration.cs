using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Data.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasMany(u => u.RefreshTokens)
                .WithOne(r => r.AppUser)
                .HasForeignKey(r => r.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
