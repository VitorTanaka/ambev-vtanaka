using Ambev.DeveloperEvaluation.Application.Sales.CancelSales;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using Moq;
using Xunit;
using System;
using System.Threading;
using System.Threading.Tasks;

public class CancelSalesHandlerTests
{
    private readonly Mock<ISalesRepository> _repositoryMock;
    private readonly CancelSalesHandler _handler;

    public CancelSalesHandlerTests()
    {
        _repositoryMock = new Mock<ISalesRepository>();
        _handler = new CancelSalesHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenSaleIsCanceled()
    {
        // Arrange
        var command = new CancelSalesCommand("001");

        _repositoryMock.Setup(r => r.CancelSalesAsync(command.Number, default))
            .ReturnsAsync(true);

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Success);
        _repositoryMock.Verify(r => r.CancelSalesAsync(command.Number, default), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowValidationException_WhenRequestIsInvalid()
    {
        // Arrange
        var command = new CancelSalesCommand("");

        // Act & Assert
        await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, default));
    }

    [Fact]
    public async Task Handle_ShouldThrowKeyNotFoundException_WhenSaleNotFound()
    {
        // Arrange
        var command = new CancelSalesCommand("999");

        _repositoryMock.Setup(r => r.CancelSalesAsync(command.Number, default))
            .ReturnsAsync(false);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(command, default));
        Assert.Contains("not found", ex.Message);
    }
}