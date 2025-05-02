namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales;

public class GetSalesResult
{
    public Guid Id { get; set; }
    public string Number { get; set; } = string.Empty;
    public DateTime Date { get; private set; }
    public string Client { get; set; } = string.Empty;
    public string CompanyBranch { get; set; } = string.Empty;
}
