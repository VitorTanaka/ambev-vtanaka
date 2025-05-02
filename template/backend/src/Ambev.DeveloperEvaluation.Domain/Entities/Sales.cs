namespace Ambev.DeveloperEvaluation.Domain.Entities;
public class Sales
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Number { get;  set; }
    public DateTime Date { get; private set; }
    public string Client { get; private set; }
    public string CompanyBranch { get; private set; }
    public bool Cancel { get; private set; }

    private readonly List<SalesItem> _itens = new();
    public IReadOnlyCollection<SalesItem> Itens => _itens;

    public decimal ValorTotal => _itens.Sum(i => i.ValorTotal);

    public Sales(string number, DateTime date, string client, string companybranch)
    {
        Number = number;
        Date = date;
        Client = client;
        CompanyBranch = companybranch;
    }

    public void AddItem(string product, decimal priceUnit, int amount)
    {
        if (amount > 20)
            throw new InvalidOperationException("Não é permitido vender mais de 20 itens idênticos.");

        decimal discount = 0;
        if (amount >= 10) discount = 0.2m;
        else if (amount >= 4) discount = 0.1m;

        if (amount < 4 && discount > 0)
            throw new InvalidOperationException("Descontos não são permitidos para menos de 4 itens.");

        var item = new SalesItem(product, priceUnit, amount, discount);
        _itens.Add(item);
        LogEvento("Item adicionado à venda");
    }

    public void Cancelar()
    {
        Cancel = true;
        LogEvento("Venda cancelada");
    }

    private void LogEvento(string mensagem)
    {
        Console.WriteLine($"[EVENTO]: {mensagem} - Venda {Number}");
    }
}