using Ambev.DeveloperEvaluation.Application.Sales.GetSales;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSales;

public class CancelSalesHandler : IRequestHandler<CancelSalesCommand, CancelSalesResponse>
{
    private readonly ISalesRepository _salesRepository;

    private readonly ILogger<CancelSalesHandler> _logger;
    public CancelSalesHandler(ISalesRepository salesRepository, ILogger<CancelSalesHandler> logger)
    {
        _salesRepository = salesRepository;
        _logger = logger;
    }

    public async Task<CancelSalesResponse> Handle(CancelSalesCommand request, CancellationToken cancellationToken)
    {
        var validator = new CancelSalesValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        _logger.LogInformation("Cancelando venda de número {Number}", request.Number);

        var success = await _salesRepository.CancelSalesAsync(request.Number, cancellationToken);
        if (!success)
            throw new KeyNotFoundException($"Number {request.Number} not found");

        return new CancelSalesResponse { Success = true };
    }
}
