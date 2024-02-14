using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using WebApplication1;
using WebApplication1.Entities;
using Xunit;

namespace TestProject1
{
    public class UnitTest1
    {
        /// <summary>
        /// ������� ���������� ������ ������� �� 10.10.2010, ������� ����� ������ 10 ��������
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Number_of_sales_of_goods_whose_cost_exceeds() 
            // � ����, ��� � ����� �� ���������� ������ ����� � ������,
            // �� � � ������ ����� ������, ��� � ������ ��� ����� ������. ���� ���� � ��� ������ �� ����
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
                Name = "����",
                Price = 20
            };

            var paste = new Good()
            {
                Name = "�����",
                Price = 10
            };

            var pomade = new Good()
            {
                Name = "������",
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
            await context.SaveChangesAsync();
            var excepted =  8;
            
            // Act
            var aa = await context.Sales.ToListAsync();
            var actual = await context.Database
                .SqlQuery<int>(@$"
SELECT Sum(SalesAmount) FROM Sales
INNER JOIN Goods ON Sales.GoodId=Goods.Id
Where SaleDate = '2020-10-10' and price > 10
")
                .ToListAsync();

            // Assert
            //Assert.Equal(excepted, actual);

            //1 ������
            //
            //SELECT Sum(SalesAmount) FROM Sales
            //INNER JOIN Goods ON Sales.GoodId = Goods.Id
            //Where SaleDate = '2020-10-10' and price > 10
            //
            //2 ������
        }
    }
}