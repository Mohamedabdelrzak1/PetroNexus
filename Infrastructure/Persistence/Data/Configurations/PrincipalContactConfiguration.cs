using Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Data.Configurations
{
    public class PrincipalContactConfiguration : IEntityTypeConfiguration<PrincipalContact>
    {
        public void Configure(EntityTypeBuilder<PrincipalContact> builder)
        {
            // العلاقة مع Principal متعرّفة في PrincipalConfiguration
        }
    }
}
