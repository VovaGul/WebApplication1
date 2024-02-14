using CRUD.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1;

public class Context : DbContext
{
    public DbSet<Good> Goods { get; set; }
    public DbSet<Sale> Sales { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"server=(localdb)\mssqllocaldb;database=CrudDb;trusted_connection=true;");
    }
}