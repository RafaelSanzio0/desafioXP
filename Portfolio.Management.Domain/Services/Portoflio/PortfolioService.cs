using Microsoft.Extensions.Logging;
using OneOf;
using Portfolio.Management.Domain.Dtos.Request;
using Portfolio.Management.Domain.Dtos.Response;
using Portfolio.Management.Domain.Entities;
using Portfolio.Management.Domain.Enums;
using Portfolio.Management.Domain.Repositories;
using Portfolio.Management.Domain.Services.Investiment;
using Portfolio.Management.Domain.Validations;

namespace Portfolio.Management.Domain.Services.Portoflio
{
    public class PortfolioService(
        IDataReadRepository dataReadRepository,
        IDataWritterRepository dataWritterRepository,
        ILogger<PortfolioService> logger) : IPortfolioService
    {
        private readonly IDataReadRepository _dataReadRepository = dataReadRepository;
        private readonly IDataWritterRepository _dataWritterRepository = dataWritterRepository;
        private readonly ILogger<PortfolioService> _logger = logger;

        public async Task<OneOf<SuccessResponse, ErrorResponse>> BuyAsync(CreatePortfolioBuyRequest request)
        {
            try
            {
                var validator = new CreatePortfolioBuyRequestValidator();
                var validationResult = await validator.ValidateAsync(request);
                if (!validationResult.IsValid)
                {
                    return new ErrorResponse(ErrorCode.ValidationError, "Erro de validação nos campos da requisição");
                }

                var financialProduct = await _dataReadRepository.GetFinancialProductsByIdAsync(request.FinancialProductId);
                if (financialProduct == null)
                {
                    return new ErrorResponse(ErrorCode.FinancialProductNotFound, "Produto financeiro não encontrado");
                }

                var portfolio = await PortfolioBuyProcess(request, financialProduct);

                _logger.LogInformation("Creating transaction to user Id {1}", request.UserId);
                await _dataWritterRepository.CreateTransactionAsync(request);

                return new SuccessResponse("Compra realizada com sucesso!", portfolio.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during processing of the BuyAsync method");
                throw;
            }
        }

        private async Task<PortfolioEntity> PortfolioBuyProcess(CreatePortfolioBuyRequest request, FinancialProductEntity financialProduct)
        {
            var portfolio = await _dataReadRepository.GetPortfolioByClientAndProductAsync(request.UserId, request.FinancialProductId);
            switch (portfolio)
            {
                case null:
                    _logger.LogInformation("Adding new financial product id {0} to user Id {1}", request.FinancialProductId, request.UserId);
                    portfolio = await _dataWritterRepository.CreatePortfolioAsync(request);
                    break;

                default:
                    _logger.LogInformation("Updating financial product id {0} to user Id {1}", request.FinancialProductId, request.UserId);
                    var totalQuantity = portfolio.Quantity + request.Quantity;
                    var totalPrice = portfolio.AveragePrice * portfolio.Quantity + financialProduct.Price * request.Quantity;
                    portfolio.Quantity = totalQuantity;
                    portfolio.AveragePrice = totalPrice / totalQuantity;
                    _dataWritterRepository.UpdatePortfolio(portfolio);
                    break;
            }
            return portfolio;
        }

        public async Task<OneOf<SuccessResponse, ErrorResponse>> SellAsync(CreatePortfolioSellRequest request)
        {
            try
            {
                var validator = new CreatePortfolioSellRequestValidator();
                var validationResult = await validator.ValidateAsync(request);
                if (!validationResult.IsValid)
                {
                    return new ErrorResponse(ErrorCode.ValidationError, "Erro de validação nos campos da requisição");
                }

                var portfolio = await _dataReadRepository.GetPortfolioByClientAndProductAsync(request.UserId, request.FinancialProductId);
                if (portfolio == null || portfolio.Quantity < request.Quantity)
                {
                    return new ErrorResponse(ErrorCode.NotEnoughQuantityToSell, "Não há quantidade o suficiente para venda");
                }

                portfolio.Quantity -= request.Quantity;

                if (portfolio.Quantity <= 0)
                {
                    logger.LogInformation("Deleting financial product id {0} to user Id {1}", request.FinancialProductId, request.UserId);
                    _dataWritterRepository.DeleteProductInPortfolio(portfolio.Id);
                }
                else
                {
                    logger.LogInformation("Updating financial product id {0} to user Id {1}", request.FinancialProductId, request.UserId);
                    _dataWritterRepository.UpdatePortfolio(portfolio);
                }

                _logger.LogInformation("Creating transaction to user Id {1}", request.UserId);
                await _dataWritterRepository.CreateTransactionAsync(request);
                return new SuccessResponse("Venda realizada com sucesso!", portfolio.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during processing of the SellAsync method");
                throw;
            }
        }

        public async Task<IEnumerable<PortfolioResponse>> GetAllInvestmentsInPortfolioAsync(int userId)
        {
            try
            {
                var entity = await _dataReadRepository.GetAllInvestmentsInPortfolioAsync(userId);
                return PortfolioResponse.MapperEntityToResponse(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during processing of the GetAllInvestmentsInPortfolioAsync method");
                throw;
            }
        }
    }
}