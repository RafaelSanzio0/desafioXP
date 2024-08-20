using Portfolio.Management.Domain.Entities;

namespace Portfolio.Management.Infra.Data.SQL
{
    public class SqlStatements
    {
        public static string SelectAllFinancialProducts { get; } = $@"
            SELECT
	            [ID],
	            [NAME],
	            [TYPE],
                [PRICE],
                [EXPIRATION_DATE] {nameof(FinancialProductEntity.ExpirationDate)}
            FROM
	            [FINANCIAL_PRODUCTS] WITH (NOLOCK)
            ORDER BY
                [CREATED_AT] DESC
        ";

        public static string SelectFinancialProductById { get; } = $@"
            SELECT
	            [ID],
	            [NAME],
	            [TYPE],
                [PRICE],
                [EXPIRATION_DATE] {nameof(FinancialProductEntity.ExpirationDate)}
            FROM
	            [FINANCIAL_PRODUCTS] WITH (NOLOCK)
            WHERE
                [ID] = @Id
        ";

        public static string SelectExpiringFinancialProducts { get; } = $@"
            SELECT
	            [ID],
	            [NAME],
	            [TYPE],
                [PRICE],
                [EXPIRATION_DATE] {nameof(FinancialProductEntity.ExpirationDate)}
            FROM
                [FINANCIAL_PRODUCTS] WITH (NOLOCK)
            WHERE
                [EXPIRATION_DATE] <= @Expiration AND [EXPIRATION_DATE] > GETDATE();
        ";

        public static string SelectAdminUsers { get; } = @"
            SELECT
                [EMAIL]
            FROM
                [USERS]
            WHERE
                [TYPE] = 2;
        ";

        public static string SelectFinancialProductByUserId { get; } = $@"
            SELECT
                [ID],
                [USER_ID] {nameof(PortfolioEntity.UserId)},
                [FINANCIAL_PRODUCT_ID] {nameof(PortfolioEntity.FinancialProductId)},
                [QUANTITY],
                [AVERAGEPRICE]
            FROM 
                [PORTFOLIOS] WITH (NOLOCK)
            WHERE
                [USER_ID] = @UserId AND [FINANCIAL_PRODUCT_ID] = @FinancialProductId
        ";

        public static string SelectAllInvestmentsInPortfolio { get; } = $@"
            SELECT 
                P.ID,
                P.FINANCIAL_PRODUCT_ID {nameof(PortfolioEntity.FinancialProductId)},
                FP.NAME AS ProductName,
                P.QUANTITY,
                P.AVERAGEPRICE
            FROM 
                PORTFOLIOS P WITH (NOLOCK)
            JOIN 
                FINANCIAL_PRODUCTS FP ON P.FINANCIAL_PRODUCT_ID = FP.ID
            WHERE 
                P.USER_ID = @UserId; 
        ";
    }
}
