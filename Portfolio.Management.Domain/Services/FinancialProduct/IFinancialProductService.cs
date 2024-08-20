using OneOf;
using Portfolio.Management.Domain.Dtos.Request;
using Portfolio.Management.Domain.Dtos.Response;

namespace Portfolio.Management.Domain.Services.FinancialProduct
{
    public interface IFinancialProductService
    {
        Task<SuccessResponse> CreateFinancialProductAsync(CreateFinancialProductRequest request);

        Task<OneOf<SuccessResponse, ErrorResponse>> UpdateFinancialProductAsync(UpdateFinancialProductRequest request, int id);

        Task<OneOf<SuccessResponse, ErrorResponse>> DeleteFinancialProductAsync(int id);

        Task<IEnumerable<FinancialProductsResponse>> GetAllFinancialProductsAsync();
    }
}