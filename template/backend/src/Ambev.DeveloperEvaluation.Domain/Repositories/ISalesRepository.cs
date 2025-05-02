using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;
public interface ISalesRepository
{
    Task<Sales> CreateSalesAsync(Sales sales, CancellationToken cancellationToken = default);
    Task<Sales> GetByNumberAsync(string number, CancellationToken cancellationToken = default);
    Task<Sales> GetAll();
    Task<bool> CancelSalesAsync(string number, CancellationToken cancellationToken = default);
}
