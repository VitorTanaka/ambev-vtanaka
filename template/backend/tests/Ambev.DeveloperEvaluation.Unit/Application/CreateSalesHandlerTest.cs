using Ambev.DeveloperEvaluation.Application.Sales.CreateSales;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using Moq;
using Xunit;
using System;
using System.Threading;
using System.Threading.Tasks;

public class CreateSalesHandlerTest
{
    private readonly Mock<ISalesRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly CreateSalesHandler _handler;

    public CreateSalesHandlerTest()
    {
        _repositoryMock = new Mock<ISalesRepository>();
        _mapperMock = new Mock<IMapper>();
        _handler = new CreateSalesHandler(_repositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldCreateAndReturnSales_WhenRequestIsValidAndNotExists()
    {
        // Arrange
        var command = new CreateSalesCommand
        {
            Number = "001"
        };

        var domainSales = new Sales("001", DateTime.Now,"Fulano","Ambev");
        var createdSales = new Sales("001", DateTime.Now, "Fulano", "Ambev");
        var expectedResult = new CreateSalesResult { Id = new Guid()};

        _repositoryMock.Setup(r => r.GetByNumberAsync(command.Number, default))
            .ReturnsAsync((Sales)null!);

        _mapperMock.Setup(m => m.Map<Sales>(command)).Returns(domainSales);

        _repositoryMock.Setup(r => r.CreateSalesAsync(domainSales, default))
            .ReturnsAsync(createdSales);

        _mapperMock.Setup(m => m.Map<CreateSalesResult>(createdSales)).Returns(expectedResult);

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(new Guid(), result.Id);
        _repositoryMock.Verify(r => r.CreateSalesAsync(It.IsAny<Sales>(), default), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowValidationException_WhenCommandIsInvalid()
    {
        // Arrange
        var command = new CreateSalesCommand
        {
            Number = ""
        };

        // Act & Assert
        await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, default));
    }

    [Fact]
    public async Task Handle_ShouldThrowInvalidOperationException_WhenSaleAlreadyExists()
    {
        // Arrange
        var command = new CreateSalesCommand { Number = "001" };
        var existingSale = new Sales("001", DateTime.Now, "Fulano", "Ambev");

        _repositoryMock.Setup(r => r.GetByNumberAsync(command.Number, default))
            .ReturnsAsync(existingSale);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(command, default));
        Assert.Contains("already exists", exception.Message);
    }
}
