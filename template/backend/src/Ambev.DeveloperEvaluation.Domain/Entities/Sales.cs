namespace Ambev.DeveloperEvaluation.Domain.Entities;
public class Sales
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Numero { get; private set; }
    public DateTime Data { get; private set; }
    public string Cliente { get; private set; }
    public string Filial { get; private set; }
    public bool Cancelada { get; private set; }

    private readonly List<SalesItem> _itens = new();
    public IReadOnlyCollection<SalesItem> Itens => _itens;

    public decimal ValorTotal => _itens.Sum(i => i.ValorTotal);

    public Sales(string numero, DateTime data, string cliente, string filial)
    {
        Numero = numero;
        Data = data;
        Cliente = cliente;
        Filial = filial;
    }

    public void AddItem(string produto, decimal precoUnitario, int quantidade)
    {
        if (quantidade > 20)
            throw new InvalidOperationException("Não é permitido vender mais de 20 itens idênticos.");

        decimal desconto = 0;
        if (quantidade >= 10) desconto = 0.2m;
        else if (quantidade >= 4) desconto = 0.1m;

        if (quantidade < 4 && desconto > 0)
            throw new InvalidOperationException("Descontos não são permitidos para menos de 4 itens.");

        var item = new SalesItem(produto, precoUnitario, quantidade, desconto);
        _itens.Add(item);
        LogEvento("Item adicionado à venda");
    }

    public void Cancelar()
    {
        Cancelada = true;
        LogEvento("Venda cancelada");
    }

    private void LogEvento(string mensagem)
    {
        Console.WriteLine($"[EVENTO]: {mensagem} - Venda {Numero}");
    }
}