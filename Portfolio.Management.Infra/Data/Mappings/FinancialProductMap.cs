using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Portfolio.Management.Domain.Entities;

namespace Portfolio.Management.Infra.Data.Mappings
{
    public class FinancialProductMap : IEntityTypeConfiguration<FinancialProductEntity>
    {
        public void Configure(EntityTypeBuilder<FinancialProductEntity> builder)
        {
            builder.ToTable("FINANCIAL_PRODUCTS");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Name);
            builder.Property(u => u.Type);
            builder.Property(u => u.Price).HasColumnType("decimal(18,2)");
            builder.Property(u => u.ExpirationDate).HasColumnName("EXPIRATION_DATE");
            builder.Property(u => u.CreatedAt).HasColumnName("CREATED_AT");
            builder.Property(u => u.UpdatedAt).HasColumnName("UPDATED_AT");
            builder.Property(u => u.CreatedBy).HasColumnName("CREATED_BY");
        }
    }
}
