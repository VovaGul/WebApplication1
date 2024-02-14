using WebApplication1;
using WebApplication1.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<Context>();

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

var cream = new Good()
{
    Id = 1,
    Name = "Крем",
    Price = 10
};

var paste = new Good()
{
    Id = 2,
    Name = "Паста",
    Price = 20
};

var pomade = new Good()
{
    Id = 3,
    Name = "Помада",
    Price = 3
}; 
app.Services.GetRequiredService<Context>().Goods.AddRangeAsync(new List<Good>()
{
    cream,
    paste,
    pomade
});

app.Services.GetRequiredService<Context>().Sales.AddRangeAsync(new List<Sale>()
{
    new Sale()
    {
        Good = cream,
        SalesAmount = 3
    },
    new Sale()
    {
        Good = cream,
        SalesAmount = 5
    },
    new Sale()
    {
        Good = paste,
        SalesAmount = 2
    },
    new Sale()
    {
        Good = pomade,
        SalesAmount = 10
    }
});

app.Services.GetRequiredService<Context>().Database.EnsureCreated();

app.Run();
