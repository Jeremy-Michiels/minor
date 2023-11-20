
using Microsoft.EntityFrameworkCore;

namespace minor;

public class MyContext : DbContext
{
    public MyContext(DbContextOptions<MyContext> options)
        : base(options)
    {
    }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.UseSqlite("Data Source=database.db");
    // }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
    public DbSet<APIKey> aPIKeys {get;set;}

}