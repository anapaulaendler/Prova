//Fornecedor: Id, Nome, Contato, Endereco
public class Fornecedor
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string Nome { get; set; }
    public required string Contato { get; set; }
    public string? Endereco { get; set; }
}