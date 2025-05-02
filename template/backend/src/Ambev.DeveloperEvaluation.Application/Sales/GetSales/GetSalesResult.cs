namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales;

public class GetSalesResult
{
    public Guid Id { get; set; }
    public string Numero { get; set; } = string.Empty;
    public DateTime Data { get; private set; }
    public string Cliente { get; set; } = string.Empty;
    public string Filial { get; set; } = string.Empty;
}
