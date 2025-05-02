namespace Ambev.DeveloperEvaluation.Domain.Entities;
public class SalesItem
{
    public string Product { get; }
    public decimal PriceUnit { get; }
    public int Amount { get; }
    public decimal Discount { get; }
    public decimal ValorTotal => Amount * PriceUnit * (1 - Discount);
    public SalesItem(string product, decimal priceUnit, int amount, decimal discount)
    {
        Product = product;
        PriceUnit = priceUnit;
        Amount = amount;
        Discount = discount;
    }
}