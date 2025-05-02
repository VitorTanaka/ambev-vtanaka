using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Application.Users.DeleteUser;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSales;

public class CancelSalesHandler : IRequestHandler<CancelSalesCommand, CancelSalesResponse>
{
    private readonly ISalesRepository _salesRepository;

    public CancelSalesHandler(
        ISalesRepository salesRepository)
    {
        _salesRepository = salesRepository;
    }

    public async Task<CancelSalesResponse> Handle(CancelSalesCommand request, CancellationToken cancellationToken)
    {
        var validator = new CancelSalesValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var success = await _salesRepository.CancelSalesAsync(request.Number, cancellationToken);
        if (!success)
            throw new KeyNotFoundException($"Number {request.Number} not found");

        return new CancelSalesResponse { Success = true };
    }
}
