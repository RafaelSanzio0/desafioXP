using Microsoft.AspNetCore.Mvc;
using Portfolio.Management.Domain.Dtos.Request;
using Portfolio.Management.Domain.Services.Investiment;

namespace Portfolio.Management.API.Controllers
{
    /// <summary>
    /// Controller responsável na compra/venda e consulta de investimentos no portoflio do cliente
    /// </summary>
    [ApiController]
    [Route("/v1/portfolio/investment")]
    public class PortfolioController(IPortfolioService portfolioService) : ControllerBase
    {
        private readonly IPortfolioService _portfolioService = portfolioService;


        /// <summary>
        /// Lista todos os produtos de investimento de um usuário
        /// </summary>
        /// <param name="userId">request:</param>
        /// <remarks>
        /// | Nome                                 | Tipo                  | Descrição                                                                                                 | Obrigatório                                |
        /// | ------------------------------------ | ----------------------| ----------------------------------------------------------------------------------------------------------|--------------------------------------------|
        /// | `UserId`                             | `int`                 | Id do usuário que esta consultando os investimentos                                                       | ✅                                        |
        /// </remarks>
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetPortfolioByClientIdAsync(int userId)
        {
            var result = await _portfolioService.GetAllInvestmentsInPortfolioAsync(userId);
            return Ok(result);
        }

        /// <summary>
        /// Realiza uma compra de um produto financeiro
        /// </summary>
        /// <param name="request">request:</param>
        /// <remarks>
        /// | Nome                                 | Tipo                  | Descrição                                                                                                 | Obrigatório                                |
        /// | ------------------------------------ | ----------------------| ----------------------------------------------------------------------------------------------------------|--------------------------------------------|
        /// | `UserId`                             | `int`                 | Id do usuário que esta realizando a compra                                                                | ✅                                        |
        /// | `FinancialProductId`                 | `int`                 | Id do produto financeiro que esta sendo comprado                                                          | ✅                                        |
        /// | `Quantity`                           | `int`                 | Quantidade do produto financeiro                                                                          | ✅                                        |
        /// | `AveragePrice`                       | `decimal`             | Preço médio do produto financeiro                                                                         | ✅                                        |
        /// </remarks>
        [HttpPost("buy")]
        public async Task<IActionResult> BuyFinancialProductAsync([FromBody] CreatePortfolioBuyRequest request)
        {
            var result = await _portfolioService.BuyAsync(request);
            return result.Match<IActionResult>
            (
                success => Ok(success),
                error => UnprocessableEntity(error)
            );   
        }

        /// <summary>
        /// Realiza uma venda de um produto financeiro
        /// </summary>
        /// <param name="request">request:</param>
        /// <remarks>
        /// | Nome                                 | Tipo                  | Descrição                                                                                                 | Obrigatório                                |
        /// | ------------------------------------ | ----------------------| ----------------------------------------------------------------------------------------------------------|--------------------------------------------|
        /// | `UserId`                             | `int`                 | Id do usuário que esta realizando a venda                                                                | ✅                                        |
        /// | `FinancialProductId`                 | `int`                 | Id do produto financeiro que esta sendo vendido                                                          | ✅                                        |
        /// | `Quantity`                           | `int`                 | Quantidade do produto financeiro                                                                          | ✅                                        |
        /// </remarks>
        [HttpPost("sell")]
        public async Task<IActionResult> SellFinancialProductAsync([FromBody] CreatePortfolioSellRequest request)
        {
            var result = await _portfolioService.SellAsync(request);
            return result.Match<IActionResult>
            (
                success => Ok(success),
                error => UnprocessableEntity(error)
            );
        }
    }
}
