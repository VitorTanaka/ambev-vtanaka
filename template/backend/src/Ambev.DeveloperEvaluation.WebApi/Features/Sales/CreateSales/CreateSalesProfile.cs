using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSales;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSales;

public class CreateSalesProfile : Profile
{
    public CreateSalesProfile()
    {
        CreateMap<CreateSalesRequest, CreateSalesCommand>();
        CreateMap<CreateSalesResult, CreateSalesResponse>();
    }
}
