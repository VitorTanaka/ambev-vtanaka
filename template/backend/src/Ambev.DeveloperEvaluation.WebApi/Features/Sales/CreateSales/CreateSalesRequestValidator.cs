using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSales;

public class CreateSalesRequestValidator : AbstractValidator<CreateSalesRequest>
{
    public CreateSalesRequestValidator()
    {
        RuleFor(x => x.Number).NotEmpty();
        RuleFor(x => x.Date).NotEmpty().WithMessage("Field required");
        RuleFor(x => x.Client).NotEmpty();
        RuleFor(x => x.CompanyBranch).NotEmpty();
    }
}