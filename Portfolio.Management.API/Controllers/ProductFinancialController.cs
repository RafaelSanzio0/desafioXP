using Microsoft.AspNetCore.Mvc;
using Portfolio.Management.Domain.Dtos.Request;
using Portfolio.Management.Domain.Services.FinancialProduct;

namespace Portfolio.Management.API.Controllers
{
    /// <summary>
    /// Controller responsável na gestão dos produtos de investimentos
    /// </summary>
    [ApiController]
    [Route("/v1/financialproducts")]
    public sealed class ProductFinancialController(IFinancialProductService service) : ControllerBase
    {
        readonly IFinancialProductService _service = service;

        /// <summary>
        /// Retorna uma lista com todos os produtos financeiros disponiveis
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllFinancialProductsAsync()
        {
            var result = await _service.GetAllFinancialProductsAsync();
            return Ok(result);
        }

        /// <summary>
        /// Cria um produto financeiro
        /// </summary>
        /// <param name="request">request:</param>
        /// <remarks>
        /// | Nome                                 | Tipo                  | Descrição                                                                                                 | Obrigatório                                |
        /// | ------------------------------------ | ----------------------| ----------------------------------------------------------------------------------------------------------|--------------------------------------------|
        /// | `Name`                               | `string`              | Nome do produto financeiro                                                                                | ✅                                        |
        /// | `Type`                               | `int`                 | Tipo do produto financeiro (1- Stock, 2- Bond, 3- FII, 4- Crypto, 5- CDB, 6- Other )                      | ✅                                        |
        /// | `Price`                              | `decimal`             | Preço do produto financeiro                                                                               | ✅                                        |
        /// | `ExpirationDate`                     | `datetime`            | Data de expiração do produto financeiro                                                                   | ✅                                        |
        /// </remarks>
        [HttpPost]
        public async Task<IActionResult> CreateFinancialProductAsync([FromBody] CreateFinancialProductRequest request)
        {
            var result = await _service.CreateFinancialProductAsync(request);
            return Ok(result);
        }

        /// <summary>
        /// Atualiza um produto financeiro
        /// </summary>
        /// <param name="request">request:</param>
        /// <param name="id">request:</param>
        /// <remarks>
        /// | Nome                                 | Tipo       | Descrição                                                                                                 | Obrigatório                                |
        /// | ------------------------------------ | ---------- | ----------------------------------------------------------------------------------------------------------|--------------------------------------------|
        /// | `Name`                               | `string`   | Nome do produto financeiro                                                                                | ❌                                        |
        /// | `Price`                              | `decimal`  | Preço do produto financeiro                                                                               | ❌                                        |
        /// | `ExpirationDate`                     | `datetime` | Data de expiração do produto financeiro                                                                   | ❌                                        |
        /// | `Id`                                 | `int`      | id do produto financeiro                                                                                  | ✅                                        |
        /// </remarks>
        [HttpPost]
        [Route("{id}")]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UpdateFinancialProductRequest request, int id)
        {
            var result = await _service.UpdateFinancialProductAsync(request, id);
            return result.Match<IActionResult>
            (
                success => Ok(success),
                error => UnprocessableEntity(error)
            );
        }

        /// <summary>
        /// Deleta um produto financeiro
        /// </summary>
        /// <param name="id">request:</param>
        /// <remarks>
        /// | Nome                                 | Tipo       | Descrição                                                                                                 | Obrigatório                                |
        /// | ------------------------------------ | ---------- | ----------------------------------------------------------------------------------------------------------|--------------------------------------------|
        /// | `Id`                                 | `int`      | id do produto financeiro                                                                                  | ✅                                         |
        /// </remarks>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            var result = await _service.DeleteFinancialProductAsync(id);
            return result.Match<IActionResult>(
                success => Ok(success),
                error => UnprocessableEntity(error)
            );
        }
    }
}
