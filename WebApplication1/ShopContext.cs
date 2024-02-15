using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebApplication1.Entities;

namespace WebApplication1;

public class ShopContext : DbContext
{
    public DbSet<Good> Goods { get; set; }
    public DbSet<Sale> Sales { get; set; }

    public ShopContext(DbContextOptions<ShopContext> options) : base(options)
    {
    }
}