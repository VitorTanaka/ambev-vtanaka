namespace Ambev.DeveloperEvaluation.Domain.Entities;
public class SalesItem
{
    public string Produto { get; }
    public decimal PrecoUnitario { get; }
    public int Quantidade { get; }
    public decimal Desconto { get; }
    public decimal ValorTotal => Quantidade * PrecoUnitario * (1 - Desconto);
    public SalesItem(string produto, decimal preco, int quantidade, decimal desconto)
    {
        Produto = produto;
        PrecoUnitario = preco;
        Quantidade = quantidade;
        Desconto = desconto;
    }
}