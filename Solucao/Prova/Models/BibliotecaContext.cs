using Microsoft.EntityFrameworkCore;

public class BibliotecaContext : DbContext
{
    public BibliotecaContext(DbContextOptions<BibliotecaContext> options) : base(options) { }
    public DbSet<Livro> Livros { get; set; }
    public DbSet<Autor> Autores { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Autor>()
            .HasMany(l => l.Livros)
            .WithOne(a => a.Autor);
    }
}