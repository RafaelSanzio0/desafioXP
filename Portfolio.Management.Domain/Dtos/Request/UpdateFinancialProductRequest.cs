namespace Portfolio.Management.Domain.Dtos.Request
{
    public record UpdateFinancialProductRequest
    (
        string Name,
        decimal? Price,
        DateTime? ExpirationDate
    );
}