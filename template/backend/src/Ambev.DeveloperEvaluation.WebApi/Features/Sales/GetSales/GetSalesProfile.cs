using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSales;

public class GetSalesProfile : Profile
{
    public GetSalesProfile()
    {
        CreateMap<string, Application.Sales.GetSales.GetSalesCommand>()
            .ConstructUsing(num => new Application.Sales.GetSales.GetSalesCommand(num));
    }
}
