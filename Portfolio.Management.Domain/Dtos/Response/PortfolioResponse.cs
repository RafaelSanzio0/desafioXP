using Portfolio.Management.Domain.Entities;

namespace Portfolio.Management.Domain.Dtos.Response
{
    public record PortfolioResponse
    (
        int Id,
        string ProductName,
        int FinancialProductId,
        int Quantity,
        decimal AveragePrice
    )
    {
        public static IEnumerable<PortfolioResponse>
            MapperEntityToResponse(IEnumerable<PortfolioEntity> entities)
        {
            return entities.Select(entities => new PortfolioResponse
            (
                entities.Id,
                entities.ProductName,
                entities.FinancialProductId,
                entities.Quantity,
                entities.AveragePrice
            ));
        }
    };
}