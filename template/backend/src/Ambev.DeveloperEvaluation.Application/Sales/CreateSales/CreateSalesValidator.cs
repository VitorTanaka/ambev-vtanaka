using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSales;

public class CreateSalesCommandValidator : AbstractValidator<CreateSalesCommand>
{
    public CreateSalesCommandValidator()
    {
        RuleFor(x => x.Numero).NotEmpty();
        RuleFor(x => x.Data).NotEmpty().WithMessage("campo obrigatório");
        RuleFor(x => x.Cliente).NotEmpty();
        RuleFor(x => x.Filial).NotEmpty();
    }
}