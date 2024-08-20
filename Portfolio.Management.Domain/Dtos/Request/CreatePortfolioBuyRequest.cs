namespace Portfolio.Management.Domain.Dtos.Request
{
    public record CreatePortfolioBuyRequest
    (
        int UserId,
        int FinancialProductId,
        int Quantity,
        decimal AveragePrice
    );
}