using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSales;

public class CancelSalesRequestValidator : AbstractValidator<CancelSalesRequest>
{
    public CancelSalesRequestValidator()
    {
        RuleFor(x => x.Number)
            .NotEmpty()
            .WithMessage("Sales Number is required");
    }
}
