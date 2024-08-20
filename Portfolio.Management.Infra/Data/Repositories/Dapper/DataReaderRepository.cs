using Dapper;
using Microsoft.Data.SqlClient;
using Portfolio.Management.Domain.Entities;
using Portfolio.Management.Domain.Repositories;
using Portfolio.Management.Infra.Data.SQL;

namespace Portfolio.Management.Infra.Data.Repositories.Dapper
{
    public class DataReaderRepository(string connectionString) : IDataReadRepository
    {
        private readonly string _connectionString = connectionString;

        public async Task<IEnumerable<FinancialProductEntity>> GetAllFinancialProductsAsync()
        {
            using var sqlConnection = new SqlConnection(_connectionString);
            return await sqlConnection.QueryAsync<FinancialProductEntity>(
                SqlStatements.SelectAllFinancialProducts
            );
        }

        public async Task<FinancialProductEntity> GetFinancialProductsByIdAsync(int id)
        {
            using var sqlConnection = new SqlConnection(_connectionString);
            return await sqlConnection.QueryFirstOrDefaultAsync<FinancialProductEntity>(
                SqlStatements.SelectFinancialProductById,
                new { Id = id }
            );
        }

        public async Task<IEnumerable<FinancialProductEntity>> GetExpiringFinancialProductsAsync(DateTime expiration)
        {
            using var sqlConnection = new SqlConnection(_connectionString);
            return await sqlConnection.QueryAsync<FinancialProductEntity>(
                SqlStatements.SelectExpiringFinancialProducts,
                new { Expiration = expiration }
            );
        }

        public async Task<IEnumerable<string>> GetAdminUsersAsync()
        {
            using var sqlConnection = new SqlConnection(_connectionString);
            return await sqlConnection.QueryAsync<string>(
                SqlStatements.SelectAdminUsers);
        }

        public async Task<PortfolioEntity> GetPortfolioByClientAndProductAsync(int userId, int financialProductId)
        {
            using var sqlConnection = new SqlConnection(_connectionString);
            return await sqlConnection.QueryFirstOrDefaultAsync<PortfolioEntity>(
                SqlStatements.SelectFinancialProductByUserId,
                new { UserId = userId, FinancialProductId = financialProductId }
            );
        }

        public async Task<IEnumerable<PortfolioEntity>> GetAllInvestmentsInPortfolioAsync(int userId)
        {
            using var sqlConnection = new SqlConnection(_connectionString);
            return await sqlConnection.QueryAsync<PortfolioEntity>(
                SqlStatements.SelectAllInvestmentsInPortfolio,
                new { UserId = userId }
            );
        }
    }
}
