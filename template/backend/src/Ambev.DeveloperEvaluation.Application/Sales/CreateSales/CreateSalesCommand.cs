using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSales;
public class CreateSalesCommand : IRequest<CreateSalesResult>
{
    public string Numero { get; set; } = string.Empty;
    public DateTime Data { get; private set; }
    public string Cliente { get; set; } = string.Empty;
    public string Filial { get; set; } = string.Empty;


    public ValidationResultDetail Validate()
    {
        var validator = new CreateSalesCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}