using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSales;

public class CancelSalesProfile : Profile
{
    public CancelSalesProfile()
    {
        CreateMap<string, Application.Sales.CancelSales.CancelSalesCommand>()
            .ConstructUsing(number => new Application.Sales.CancelSales.CancelSalesCommand(number));
    }
}
