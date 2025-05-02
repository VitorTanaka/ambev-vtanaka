using Ambev.DeveloperEvaluation.Application.Sales.GetSales;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using Moq;
using Xunit;

public class GetSalesHandlerTest
{
    private readonly Mock<ISalesRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetSalesHandler _handler;

    public GetSalesHandlerTest()
    {
        _repositoryMock = new Mock<ISalesRepository>();
        _mapperMock = new Mock<IMapper>();
        _handler = new GetSalesHandler(_repositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnMappedResult_WhenSaleExists()
    {
        // Arrange
        var command = new GetSalesCommand("12345");
        var sale = new Sales("12345", DateTime.Now, "Fulano", "Ambev");
        var expectedResult = new GetSalesResult { Number = "12345" };

        _repositoryMock.Setup(r => r.GetByNumberAsync(command.Number, default))
            .ReturnsAsync(sale);
        _mapperMock.Setup(m => m.Map<GetSalesResult>(sale))
            .Returns(expectedResult);

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedResult.Number, result.Number);
    }

    [Fact]
    public async Task Handle_ShouldThrowValidationException_WhenRequestIsInvalid()
    {
        // Arrange
        var command = new GetSalesCommand("");

        // Act & Assert
        await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, default));
    }

    [Fact]
    public async Task Handle_ShouldThrowKeyNotFoundException_WhenSaleNotFound()
    {
        // Arrange
        var command = new GetSalesCommand("99999");

        _repositoryMock.Setup(r => r.GetByNumberAsync(command.Number, default))
            .ReturnsAsync((Sales)null!);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(command, default));
        Assert.Contains("not found", exception.Message);
    }
}