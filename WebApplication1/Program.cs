using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using WebApplication1;
using WebApplication1.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection(nameof(DatabaseSettings)));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ShopContext>((provider, contextBuilder)=> contextBuilder
    .UseSqlServer(provider.GetRequiredService<IOptions<DatabaseSettings>>().Value.ConnectionString));
builder.Services.AddLogging();

var app = builder.Build();

app.UseMiddleware<RequestLoggingMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ShopContext>();
    await context.Database.EnsureDeletedAsync();
    await context.Database.EnsureCreatedAsync();
    var cream = new Good()
    {
        Name = "крем",
        Price = 20,
        Category = "косметика",
        Weight = 10
    };

    var paste = new Good()
    {
        Name = "паста",
        Price = 10,
        Category = "машины",
        Weight = 15
    };

    var pomade = new Good()
    {
        Name = "помада",
        Price = 3,
        Category = "косметика",
        Weight = 5
    };
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
            SalesAmount = 3000,
            SaleDate = new DateTime(2020, 10, 10)
        },
        new Sale()
        {
            Good = cream,
            SalesAmount = 500,
            SaleDate = new DateTime(2020, 10, 10)
        },
        new Sale()
        {
            Good = cream,
            SalesAmount = 50,
            SaleDate = new DateTime(2020, 10, 11)
        },
        new Sale()
        {
            Good = cream,
            SalesAmount = 2000,
            SaleDate = DateTime.Now
        },
        new Sale()
        {
            Good = paste,
            SalesAmount = 2000,
            SaleDate = new DateTime(2020, 10, 11)
        },
        new Sale()
        {
            Good = pomade,
            SalesAmount = 10,
            SaleDate = new DateTime(2020, 10, 11)
        },
        new Sale()
        {
            Good = pomade,
            SalesAmount = 2000,
            SaleDate = DateTime.Now
        }
    });
    await context.SaveChangesAsync();
}

app.Run();
