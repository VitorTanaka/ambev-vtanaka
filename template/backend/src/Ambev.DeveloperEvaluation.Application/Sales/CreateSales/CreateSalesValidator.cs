using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSales;

public class CreateSalesCommandValidator : AbstractValidator<CreateSalesCommand>
{
    public CreateSalesCommandValidator()
    {
        RuleFor(x => x.Number).NotEmpty();
        RuleFor(x => x.Date).NotEmpty().WithMessage("campo obrigatório");
        RuleFor(x => x.Client).NotEmpty();
        RuleFor(x => x.CompanyBranch).NotEmpty();
    }
}