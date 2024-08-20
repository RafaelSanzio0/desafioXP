using Portfolio.Management.Domain.Dtos.Request;
using Portfolio.Management.Domain.Entities;

namespace Portfolio.Management.Domain.Repositories
{
    public interface IDataWritterRepository
    {
        Task<FinancialProductEntity> CreateFinancialProductAsync(CreateFinancialProductRequest request);

        Task<FinancialProductEntity> UpdateFinancialProductAsync(UpdateFinancialProductRequest request, FinancialProductEntity financialProduct);

        Task DeleteFinancialProductAsync(int id);

        Task<PortfolioEntity> CreatePortfolioAsync(CreatePortfolioBuyRequest request);

        PortfolioEntity UpdatePortfolio(PortfolioEntity portfolio);

        Task<TransactionEntity> CreateTransactionAsync(CreatePortfolioBuyRequest request);

        Task<TransactionEntity> CreateTransactionAsync(CreatePortfolioSellRequest request);

        void DeleteProductInPortfolio(int id);
    }
}