//Produto: Id, Nome, Descricao, Preco, Quantidade, FornecedorId.
public class Produto
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string Nome { get; set; }
    public string? Descricao { get; set; }
    public double Preco { get; set; }
    public int Quantidade { get; set; }
    public required Fornecedor Fornecedor { get; set; }
}