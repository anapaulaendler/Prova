//Paciente: Id, Nome, Idade, Sexo, Telefone, Endereco.
public class Paciente
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string Nome { get; set; }
    public int Idade { get; set; }
    public required char Sexo { get; set; }
    public required string Telefone { get; set; }
    public required string Endereco { get; set; }
}