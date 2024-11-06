public class Livro
{
    public string LivroId { get; set; } = Guid.NewGuid().ToString();
    public required string Titulo { get; set; }
    public int AnoPublicacao { get; set; }
    public Autor? Autor { get; set; }
    public string? AutorId { get; set; }
}