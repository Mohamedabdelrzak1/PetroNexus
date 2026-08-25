using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Data.Configurations
{
    public class DocumentRecordConfiguration : IEntityTypeConfiguration<DocumentRecord>
    {
        public void Configure(EntityTypeBuilder<DocumentRecord> builder)
        {
            builder.HasOne(d => d.UploadedByUser)
                .WithMany()
                .HasForeignKey(d => d.UploadedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(d => d.Signatures)
                .WithOne(s => s.DocumentRecord)
                .HasForeignKey(s => s.DocumentRecordId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
