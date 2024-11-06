//Pedido: Id, ProdutoId, Quantidade, DataPedido, Status

public class Pedido
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required Produto Produto { get; set; }
    public int Quantidade { get; set; }
    public DateTime DataPedido { get; set; }
    public bool Status { get; set; }
}