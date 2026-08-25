using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Data.Configurations
{
    public class EmployeeDocumentConfiguration : IEntityTypeConfiguration<EmployeeDocument>
    {
        public void Configure(EntityTypeBuilder<EmployeeDocument> builder)
        {
            // العلاقة مع Employee متعرّفة في EmployeeConfiguration
        }
    }
}
