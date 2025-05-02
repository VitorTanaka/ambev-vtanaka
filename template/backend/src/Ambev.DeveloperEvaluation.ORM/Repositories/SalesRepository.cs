using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class SalesRepository : ISalesRepository
{
    private readonly DefaultContext _context;
    public SalesRepository(DefaultContext context)
    {
        _context = context;
    }
    public async Task<Sales> CreateSalesAsync(Sales sales, CancellationToken cancellationToken = default)
    {
        await _context.Sales.AddAsync(sales, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return sales;
    }

    public async Task<Sales?> GetByNumberAsync(string number, CancellationToken cancellationToken = default)
    {
        return await _context.Sales
            .FirstOrDefaultAsync(x => x.Number == number, cancellationToken);
    }

    public async Task<Sales> GetAll() => await _context.Sales.FirstOrDefaultAsync();

    public async Task<bool> CancelSalesAsync(string number, CancellationToken cancellationToken = default)
    {
        var _sales = await GetByNumberAsync(number, cancellationToken);
        if (_sales == null)
            return false;

        _context.Sales.Update(_sales);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
