using Ambev.DeveloperEvaluation.Application.Sales.CreateSales;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSales;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings;

public class CreateSalesRequestProfile : Profile
{
    public CreateSalesRequestProfile()
    {
        CreateMap<CreateSalesRequest, CreateSalesCommand>();
    }
}