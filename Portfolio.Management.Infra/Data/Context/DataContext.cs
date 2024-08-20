using Microsoft.EntityFrameworkCore;
using Portfolio.Management.Domain.Entities;
using Portfolio.Management.Infra.Data.Mappings;

namespace Portfolio.Management.Infra.Data.Context
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        public DbSet<FinancialProductEntity> FinancialProducts { get; set; }
        public DbSet<PortfolioEntity> Portfolios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new FinancialProductMap());
            modelBuilder.ApplyConfiguration(new PortfolioMap());
            modelBuilder.ApplyConfiguration(new TransactionMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
