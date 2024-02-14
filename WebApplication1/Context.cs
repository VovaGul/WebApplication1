using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebApplication1.Entities;

namespace WebApplication1;

public class Context : DbContext
{
    public DbSet<Good> Goods { get; set; }
    public DbSet<Sale> Sales { get; set; }

    public Context(DbContextOptions<Context> options) : base(options)
    {
    }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseSqlServer(@"server=(localdb)\mssqllocaldb;database=CrudDb;trusted_connection=true;");
    //}
}