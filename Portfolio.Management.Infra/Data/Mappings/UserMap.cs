using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Portfolio.Management.Domain.Entities;

namespace Portfolio.Management.Infra.Data.Mappings
{
    public class UserMap : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("USERS");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Name);
            builder.Property(u => u.Email);
            builder.Property(u => u.Type);
            builder.Property(u => u.CreatedAt).HasColumnName("CREATED_AT");
            builder.Property(u => u.UpdatedAt).HasColumnName("UPDATED_AT");
            builder.Property(u => u.CreatedBy).HasColumnName("CREATED_BY");
        }
    }
}
