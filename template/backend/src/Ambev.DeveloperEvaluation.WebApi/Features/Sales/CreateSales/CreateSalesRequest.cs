using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSales;

public class CreateSalesRequest
{
    public string Number { get; set; } = string.Empty;
    public DateTime Date { get; private set; }
    public string Client { get; set; } = string.Empty;
    public string CompanyBranch { get; set; } = string.Empty;
}