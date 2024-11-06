using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<Fornecedor> TabelaFornecedores { get; set; } 
    public DbSet<Pedido> TabelaPedidos { get; set; } 
    public DbSet<Produto> TabelaProdutos { get; set; } 

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=BancoDeDados.db");
    }
}
