using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSales;

public class CancelSalesValidator : AbstractValidator<CancelSalesCommand>
{
    public CancelSalesValidator()
    {
        RuleFor(x => x.Number)
            .NotEmpty()
            .WithMessage("Sales Number is required");
    }
}
