using Portfolio.Management.Domain.Entities;
using Portfolio.Management.Domain.Enums;

namespace Portfolio.Management.Domain.Dtos.Response
{
    public record FinancialProductsResponse
    (
        int Id,
        string Name,
        FinancialProductType Type,
        decimal Price,
        DateTime ExpirationDate
    )
    {
        public static IEnumerable<FinancialProductsResponse>
            MapperEntityToResponse(IEnumerable<FinancialProductEntity> entities)
        {
            return entities.Select(entities => new FinancialProductsResponse
            (
                entities.Id,
                entities.Name,
                entities.Type,
                entities.Price,
                entities.ExpirationDate
            ));
        }
    };
}