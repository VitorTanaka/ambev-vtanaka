namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSales;

public class GetSalesResponse
{
    public Guid Id { get; set; }
    public string Numero { get; set; } = string.Empty;
    public DateTime Data { get; private set; }
    public string Cliente { get; set; } = string.Empty;
    public string Filial { get; set; } = string.Empty;
}
