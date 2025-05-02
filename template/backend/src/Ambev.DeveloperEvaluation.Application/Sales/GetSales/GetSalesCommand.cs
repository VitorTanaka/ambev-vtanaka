using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales;

public class GetSalesCommand : IRequest<GetSalesResult>
{
    public string Number { get; }
    public GetSalesCommand(string number)
    {
        Number = number;
    }
}