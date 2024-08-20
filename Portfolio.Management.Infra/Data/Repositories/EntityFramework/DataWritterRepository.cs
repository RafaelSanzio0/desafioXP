using Microsoft.Extensions.Logging;
using Portfolio.Management.Domain.Dtos.Request;
using Portfolio.Management.Domain.Entities;
using Portfolio.Management.Domain.Enums;
using Portfolio.Management.Domain.Repositories;
using Portfolio.Management.Infra.Data.Context;

namespace Portfolio.Management.Infra.Data.Repositories.EntityFramework
{
    public class DataWritterRepository(DataContext context, ILogger<DataWritterRepository> logger) : IDataWritterRepository
    {
        private readonly DataContext _context = context;
        private readonly ILogger<DataWritterRepository> _logger = logger;

        public async Task<FinancialProductEntity> CreateFinancialProductAsync(CreateFinancialProductRequest request)
        {
            var entity = new FinancialProductEntity
            {
                Name = request.Name,
                Type = request.Type,
                Price = request.Price,
                ExpirationDate = request.ExpirationDate,
                UpdatedAt = null,
            };

            await _context.AddAsync(entity);
            await CompleteAsync();
            return entity;
        }

        public async Task DeleteFinancialProductAsync(int id)
        {
            _context.RemoveRange(_context.FinancialProducts.Where(x => x.Id == id));
            await CompleteAsync();
        }

        public async Task<FinancialProductEntity> UpdateFinancialProductAsync(UpdateFinancialProductRequest request, FinancialProductEntity entity)
        {
            var newEntity = new FinancialProductEntity
            {
                Id = entity.Id,
                CreatedAt = entity.CreatedAt,
                Type = entity.Type,
                UpdatedAt = DateTime.Now,
                Name = request.Name ?? entity.Name,
                Price = request.Price ?? entity.Price,
                ExpirationDate = request.ExpirationDate ?? entity.ExpirationDate
            };

            _context.Update(newEntity);
            await CompleteAsync();
            return newEntity;
        }

        public async Task<PortfolioEntity> CreatePortfolioAsync(CreatePortfolioBuyRequest request)
        {
            var entity = new PortfolioEntity
            {
                UserId = request.UserId,
                FinancialProductId = request.FinancialProductId,
                Quantity = request.Quantity,
                AveragePrice = request.AveragePrice
            };

            await _context.AddAsync(entity);
            return entity;
        }

        public PortfolioEntity UpdatePortfolio(PortfolioEntity portfolio)
        {
            _context.Update(portfolio);
            return portfolio;
        }

        public async Task<TransactionEntity> CreateTransactionAsync(CreatePortfolioBuyRequest request)
        {
            var transaction = new TransactionEntity
            {
                UserId = request.UserId,
                FinancialProductId = request.FinancialProductId,
                Date = DateTime.UtcNow,
                Amount = request.Quantity,
                Type = TransactionType.Buy
            };

            await _context.AddAsync(transaction);
            await CompleteAsync();
            return transaction;
        }

        public async Task<TransactionEntity> CreateTransactionAsync(CreatePortfolioSellRequest request)
        {
            var transaction = new TransactionEntity
            {
                UserId = request.UserId,
                FinancialProductId = request.FinancialProductId,
                Date = DateTime.UtcNow,
                Amount = request.Quantity,
                Type = TransactionType.Sell
            };

            await _context.AddAsync(transaction);
            await CompleteAsync();
            return transaction;
        }

        public void DeleteProductInPortfolio(int id)
        {
            _context.Remove(_context.Portfolios.Where(x => x.Id == id));
        }

        private async Task CompleteAsync()
        {
            _logger.LogInformation("Commiting changes into the database...");
            await _context.SaveChangesAsync();
        }
    }
}
