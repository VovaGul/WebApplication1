using Microsoft.EntityFrameworkCore;
using WebApplication1;
using WebApplication1.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<Context>(contextBuilder => contextBuilder
    .UseSqlServer(@"server=(localdb)\mssqllocaldb;database=CrudDb;trusted_connection=true;"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
using var a = app.Services.CreateScope();
var context = a.ServiceProvider.GetRequiredService<Context>();

var cream = new Good()
{
    Name = "Крем",
    Price = 20,
    Category = "Косметика",
    Weight = 10
};

var paste = new Good()
{
    Name = "Паста",
    Price = 10,
    Category = "Машины"
};

var pomade = new Good()
{
    Name = "Помада",
    Price = 3,
    Category = "Косметика"
};
await context.Database.EnsureDeletedAsync();
await context.Database.EnsureCreatedAsync();
await context.Goods.AddRangeAsync(new List<Good>()
{
    cream,
    paste,
    pomade
});

await context.Sales.AddRangeAsync(new List<Sale>()
{
    new Sale()
    {
        Good = cream,
        SalesAmount = 3,
        SaleDate = new DateTime(2020, 10, 10)
    },
    new Sale()
    {
        Good = cream,
        SalesAmount = 5,
        SaleDate = new DateTime(2020, 10, 10)
    },
    new Sale()
    {
        Good = cream,
        SalesAmount = 5,
        SaleDate = new DateTime(2020, 10, 11)
    },
    new Sale()
    {
        Good = paste,
        SalesAmount = 2,
        SaleDate = new DateTime(2020, 10, 11)
    },
    new Sale()
    {
        Good = pomade,
        SalesAmount = 10,
        SaleDate = new DateTime(2020, 10, 11)
    }
});
await context.SaveChangesAsync();
app.Run();
