using Portfolio.Management.Domain.Entities;

namespace Portfolio.Management.Domain.Repositories
{
    public interface IDataReadRepository
    {
        Task<IEnumerable<FinancialProductEntity>> GetAllFinancialProductsAsync();

        Task<FinancialProductEntity> GetFinancialProductsByIdAsync(int id);

        Task<IEnumerable<FinancialProductEntity>> GetExpiringFinancialProductsAsync(DateTime expiration);

        Task<IEnumerable<string>> GetAdminUsersAsync();

        Task<PortfolioEntity> GetPortfolioByClientAndProductAsync(int userId, int financialProductId);

        Task<IEnumerable<PortfolioEntity>> GetAllInvestmentsInPortfolioAsync(int userId);
    }
}