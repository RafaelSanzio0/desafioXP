using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Portfolio.Management.Domain.Entities;

namespace Portfolio.Management.Infra.Data.Mappings
{
    public class TransactionMap : IEntityTypeConfiguration<TransactionEntity>
    {
        public void Configure(EntityTypeBuilder<TransactionEntity> builder)
        {
            builder.ToTable("TRANSACTIONS");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.UserId).HasColumnName("USER_ID");
            builder.Property(u => u.FinancialProductId).HasColumnName("FINANCIAL_PRODUCT_ID");
            builder.Property(u => u.Date).HasColumnName("TRANSACTION_DATE");
            builder.Property(u => u.Amount);
            builder.Property(u => u.Type);
            builder.Property(u => u.CreatedAt).HasColumnName("CREATED_AT");
            builder.Property(u => u.UpdatedAt).HasColumnName("UPDATED_AT");
            builder.Property(u => u.CreatedBy).HasColumnName("CREATED_BY");
        }
    }
}
