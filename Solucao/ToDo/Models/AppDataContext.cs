using Microsoft.EntityFrameworkCore;

public class AppDataContext : DbContext
{
    public DbSet<Activity> TableOfActivities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Database.db");
    }
}