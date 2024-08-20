using FluentValidation;
using Portfolio.Management.Domain.Dtos.Request;

namespace Portfolio.Management.Domain.Validations
{
    public class CreatePortfolioSellRequestValidator : AbstractValidator<CreatePortfolioSellRequest>
    {
        public CreatePortfolioSellRequestValidator()
        {
            RuleFor(request => request.UserId)
                .NotEmpty().WithMessage("UserId é obrigatório");
            RuleFor(request => request.FinancialProductId)
               .NotEmpty().WithMessage("FinancialProductId é obrigatório");
            RuleFor(request => request.Quantity)
                .NotEmpty().WithMessage("Quantity é obrigatório")
                .GreaterThan(0).WithName("Quantity deve ser maior que 0");
        }
    }
}