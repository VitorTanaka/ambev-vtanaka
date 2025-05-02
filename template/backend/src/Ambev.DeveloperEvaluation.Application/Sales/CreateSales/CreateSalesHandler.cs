using Ambev.DeveloperEvaluation.Application.Sales.GetSales;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSales;

public class CreateSalesHandler : IRequestHandler<CreateSalesCommand, CreateSalesResult>
{
    private readonly ISalesRepository _salesRepository;
    private readonly IMapper _mapper;

    private readonly ILogger<CreateSalesHandler> _logger;
    public CreateSalesHandler(ISalesRepository salesRepository, IMapper mapper, ILogger<CreateSalesHandler> logger)
    {
        _salesRepository = salesRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CreateSalesResult> Handle(CreateSalesCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateSalesCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        _logger.LogInformation("Buscando venda com número {Number}", command.Number);

        var getSales = await _salesRepository.GetByNumberAsync(command.Number, cancellationToken);
        if (getSales != null)
            throw new InvalidOperationException($"Sales {command.Number} already exists");

        var _sales = _mapper.Map<Domain.Entities.Sales>(command);

        var createdSales = await _salesRepository.CreateSalesAsync(_sales, cancellationToken);

        _logger.LogInformation("Venda {Number} registrada!", createdSales.Number);

        var result = _mapper.Map<CreateSalesResult>(createdSales);
        return result;
    }
}
