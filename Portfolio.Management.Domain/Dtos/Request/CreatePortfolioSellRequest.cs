namespace Portfolio.Management.Domain.Dtos.Request
{
    public record CreatePortfolioSellRequest
    (
        int UserId,
        int FinancialProductId,
        int Quantity
    );
}