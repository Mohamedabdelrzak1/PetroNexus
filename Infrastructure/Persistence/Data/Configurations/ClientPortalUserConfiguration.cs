using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Data.Configurations
{
    public class ClientPortalUserConfiguration : IEntityTypeConfiguration<ClientPortalUser>
    {
        public void Configure(EntityTypeBuilder<ClientPortalUser> builder)
        {
            builder.HasOne(p => p.AppUser)
                .WithMany()
                .HasForeignKey(p => p.AppUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
