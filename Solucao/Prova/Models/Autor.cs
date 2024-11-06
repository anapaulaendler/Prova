public class Autor
{
    public string AutorId { get; set; } = Guid.NewGuid().ToString();
    public required string Nome { get; set; }
    public DateTime DataNascimento { get; set; }
    public ICollection<Livro> Livros { get; set; } = new List<Livro>();
}