using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales;

public class GetSalesHandler : IRequestHandler<GetSalesCommand, GetSalesResult>
{
    private readonly ISalesRepository _salesRepository;
    private readonly IMapper _mapper;

    private readonly ILogger<GetSalesHandler> _logger;
    public GetSalesHandler(
        ISalesRepository salesRepository,
        IMapper mapper, ILogger<GetSalesHandler> logger)
    {
        _salesRepository = salesRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<GetSalesResult> Handle(GetSalesCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetSalesValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        _logger.LogInformation("Buscando venda com número {Number}", request.Number);

        var sales = await _salesRepository.GetByNumberAsync(request.Number, cancellationToken);
        if (sales == null)
            throw new KeyNotFoundException($"Sales with Number {request.Number} not found");

        return _mapper.Map<GetSalesResult>(sales);
    }
}