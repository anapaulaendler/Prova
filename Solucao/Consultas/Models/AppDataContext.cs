using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<Consulta> TabelaConsultas { get; set; } 
    public DbSet<Paciente> TabelaPacientes { get; set; } 

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("BancoDeDadosEmMemoria");
    }
}
