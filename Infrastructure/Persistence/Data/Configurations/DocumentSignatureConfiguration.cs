using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Data.Configurations
{
    public class DocumentSignatureConfiguration : IEntityTypeConfiguration<DocumentSignature>
    {
        public void Configure(EntityTypeBuilder<DocumentSignature> builder)
        {
            // العلاقة مع DocumentRecord متعرّفة في DocumentRecordConfiguration (Cascade)

            builder.HasOne(s => s.SignerUser)
                .WithMany()
                .HasForeignKey(s => s.SignerUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
