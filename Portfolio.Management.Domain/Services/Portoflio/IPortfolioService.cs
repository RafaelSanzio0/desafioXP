using OneOf;
using Portfolio.Management.Domain.Dtos.Request;
using Portfolio.Management.Domain.Dtos.Response;

namespace Portfolio.Management.Domain.Services.Investiment
{
    public interface IPortfolioService
    {
        Task<OneOf<SuccessResponse, ErrorResponse>> BuyAsync(CreatePortfolioBuyRequest request);

        Task<OneOf<SuccessResponse, ErrorResponse>> SellAsync(CreatePortfolioSellRequest request);

        Task<IEnumerable<PortfolioResponse>> GetAllInvestmentsInPortfolioAsync(int userId);
    }
}