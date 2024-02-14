using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using WebApplication1;
using WebApplication1.Entities;
using Xunit;

namespace TestProject1
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1()
        {
            // Arrange

            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<Context>()
                .UseSqlite(connection)
                .Options;

            await using var context = new Context(options);

            var cream = new Good()
            {
                Id = 1,
                Name = "Крем",
                Price = 20
            };

            var paste = new Good()
            {
                Id = 2,
                Name = "Паста",
                Price = 10
            };

            var pomade = new Good()
            {
                Id = 3,
                Name = "Помада",
                Price = 3
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
            var excepted = new List<int> { 8 };

            // Act
            var actual = await context.Database
                .SqlQuery<int>(@$"
SELECT COUNT(*) AS SalesCount
FROM Sales s
JOIN Goods g ON s.GoodId = g.Id
WHERE s.SaleDate = '2010-10-10' AND g.Price > 10;
")
                .ToListAsync();

            // Assert
            Assert.Equal(excepted, actual);
        }
    }
}