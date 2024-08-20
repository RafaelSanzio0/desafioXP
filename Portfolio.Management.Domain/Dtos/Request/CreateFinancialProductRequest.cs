using Portfolio.Management.Domain.Enums;

namespace Portfolio.Management.Domain.Dtos.Request
{
    public record CreateFinancialProductRequest
    (
        string Name,
        FinancialProductType Type,
        decimal Price,
        DateTime ExpirationDate
    );
}