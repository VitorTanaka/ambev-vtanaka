using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales;

public class GetSalesHandler : IRequestHandler<GetSalesCommand, GetSalesResult>
{
    private readonly ISalesRepository _salesRepository;
    private readonly IMapper _mapper;

    public GetSalesHandler(
        ISalesRepository salesRepository,
        IMapper mapper)
    {
        _salesRepository = salesRepository;
        _mapper = mapper;
    }

    public async Task<GetSalesResult> Handle(GetSalesCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetSalesValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var sales = await _salesRepository.GetByNumberAsync(request.Number, cancellationToken);
        if (sales == null)
            throw new KeyNotFoundException($"Sales with Number {request.Number} not found");

        return _mapper.Map<GetSalesResult>(sales);
    }
}