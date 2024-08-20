using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Portfolio.Management.Domain.Entities;

namespace Portfolio.Management.Infra.Data.Mappings
{
    public class PortfolioMap : IEntityTypeConfiguration<PortfolioEntity>
    {
        public void Configure(EntityTypeBuilder<PortfolioEntity> builder)
        {
            builder.ToTable("PORTFOLIOS");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.UserId).HasColumnName("USER_ID");
            builder.Property(u => u.FinancialProductId).HasColumnName("FINANCIAL_PRODUCT_ID");
            builder.Property(u => u.Quantity);
            builder.Property(u => u.AveragePrice).HasColumnType("decimal(10,2)");
            builder.Property(u => u.CreatedAt).HasColumnName("CREATED_AT");
            builder.Property(u => u.UpdatedAt).HasColumnName("UPDATED_AT");
            builder.Property(u => u.CreatedBy).HasColumnName("CREATED_BY");
        }
    }
}
