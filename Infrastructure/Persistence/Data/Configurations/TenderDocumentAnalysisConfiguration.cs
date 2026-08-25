using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Data.Configurations
{
    public class TenderDocumentAnalysisConfiguration : IEntityTypeConfiguration<TenderDocumentAnalysis>
    {
        public void Configure(EntityTypeBuilder<TenderDocumentAnalysis> builder)
        {
            builder.HasOne(a => a.Tender)
                .WithOne(t => t.DocumentAnalysis)
                .HasForeignKey<TenderDocumentAnalysis>(a => a.TenderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.SuggestedPrincipal)
                .WithMany()
                .HasForeignKey(a => a.SuggestedPrincipalId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
