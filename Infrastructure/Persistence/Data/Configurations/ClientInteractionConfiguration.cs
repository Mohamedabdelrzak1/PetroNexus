using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Data.Configurations
{
    public class ClientInteractionConfiguration : IEntityTypeConfiguration<ClientInteraction>
    {
        public void Configure(EntityTypeBuilder<ClientInteraction> builder)
        {
            builder.HasOne(i => i.Tender)
                .WithMany()
                .HasForeignKey(i => i.TenderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.CreatedByUser)
                .WithMany()
                .HasForeignKey(i => i.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
