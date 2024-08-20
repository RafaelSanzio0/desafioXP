using Microsoft.Extensions.Logging;
using OneOf;
using Portfolio.Management.Domain.Dtos.Request;
using Portfolio.Management.Domain.Dtos.Response;
using Portfolio.Management.Domain.Enums;
using Portfolio.Management.Domain.Repositories;

namespace Portfolio.Management.Domain.Services.FinancialProduct
{
    public class FinancialProductService(
        ILogger<FinancialProductService> logger,
        IDataReadRepository dataReadRepository,
        IDataWritterRepository dataWritterRepository) : IFinancialProductService
    {
        private readonly ILogger<FinancialProductService> _logger = logger;
        private readonly IDataReadRepository _dataReadRepository = dataReadRepository;
        private readonly IDataWritterRepository _dataWritterRepository = dataWritterRepository;

        public async Task<SuccessResponse> CreateFinancialProductAsync(CreateFinancialProductRequest request)
        {
            try
            {
                var entity = await _dataWritterRepository.CreateFinancialProductAsync(request);
                return new SuccessResponse("Produto financeiro criado com sucesso!", entity.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during processing of the CreateFinancialProductAsync method");
                throw;
            }
        }

        public async Task<OneOf<SuccessResponse, ErrorResponse>> DeleteFinancialProductAsync(int id)
        {
            try
            {
                var entity = await _dataReadRepository.GetFinancialProductsByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("Financial product with id {0} not founded", id);
                    return new ErrorResponse(ErrorCode.FinancialProductNotFound, "Produto financeiro não encontrado");
                }

                await _dataWritterRepository.DeleteFinancialProductAsync(id);
                return new SuccessResponse("Produto financeiro deletado com sucesso!", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during processing of the DeleteFinancialProductAsync method");
                throw;
            }
        }

        public async Task<OneOf<SuccessResponse, ErrorResponse>> UpdateFinancialProductAsync(UpdateFinancialProductRequest request, int id)
        {
            try
            {
                var entity = await _dataReadRepository.GetFinancialProductsByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("Financial product with id {0} not founded", id);
                    return new ErrorResponse(ErrorCode.FinancialProductNotFound, "Produto financeiro não encontrado");
                }

                var newEntity = await _dataWritterRepository.UpdateFinancialProductAsync(request, entity);
                return new SuccessResponse("Produto financeiro atualizado com sucesso!", entity.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during processing of the UpdateFinancialProductAsync method");
                throw;
            }
        }

        public async Task<IEnumerable<FinancialProductsResponse>> GetAllFinancialProductsAsync()
        {
            try
            {
                var entity = await _dataReadRepository.GetAllFinancialProductsAsync();
                return FinancialProductsResponse.MapperEntityToResponse(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during processing of the GetAllFinancialProductsAsync method");
                throw;
            }
        }
    }
}