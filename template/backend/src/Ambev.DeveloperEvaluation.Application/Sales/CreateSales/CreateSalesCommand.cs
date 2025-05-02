using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSales;
public class CreateSalesCommand : IRequest<CreateSalesResult>
{
    public string Number { get; set; } = string.Empty;
    public DateTime Date { get; private set; }
    public string Client { get; set; } = string.Empty;
    public string CompanyBranch { get; set; } = string.Empty;


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