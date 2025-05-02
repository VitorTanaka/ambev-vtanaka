using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSales;

public record CancelSalesCommand : IRequest<CancelSalesResponse>
{
    public string Number { get; }
    public CancelSalesCommand(string number)
    {
        Number = number;
    }
}
