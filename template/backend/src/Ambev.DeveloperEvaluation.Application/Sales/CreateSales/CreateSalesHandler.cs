using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSales;

public class CreateSalesHandler : IRequestHandler<CreateSalesCommand, CreateSalesResult>
{
    private readonly ISalesRepository _salesRepository;
    private readonly IMapper _mapper;

    public CreateSalesHandler(ISalesRepository salesRepository, IMapper mapper)
    {
        _salesRepository = salesRepository;
        _mapper = mapper;
    }

    public async Task<CreateSalesResult> Handle(CreateSalesCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateSalesCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var getSales = await _salesRepository.GetByNumberAsync(command.Numero, cancellationToken);
        if (getSales != null)
            throw new InvalidOperationException($"Sales {command.Numero} already exists");

        var _sales = _mapper.Map<Domain.Entities.Sales>(command);

        var createdSales = await _salesRepository.CreateSalesAsync(_sales, cancellationToken);
        var result = _mapper.Map<CreateSalesResult>(createdSales);
        return result;
    }
}
