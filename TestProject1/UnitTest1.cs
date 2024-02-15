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

        /// <summary>
        /// ������� ��� 5 ����� ����������� �������
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task TASK2()
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

            var good4 = new Good()
            {
                Name = "������",
                Price = 3
            };

            var good5 = new Good()
            {
                Name = "������",
                Price = 3
            };

            var good6 = new Good()
            {
                Name = "����",
                Price = 3
            };
            await context.Database.EnsureCreatedAsync();
            await context.Goods.AddRangeAsync(new List<Good>()
            {
                cream,
                paste,
                pomade,
                good4,
                good5,
                good6
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
                },
                new Sale()
                {
                    Good = good4,
                    SalesAmount = 9,
                    SaleDate = new DateTime(2020, 10, 11)
                },
                new Sale()
                {
                    Good = good5,
                    SalesAmount = 9,
                    SaleDate = new DateTime(2020, 10, 11)
                },
                new Sale()
                {
                    Good = good6,
                    SalesAmount = 8,
                    SaleDate = new DateTime(2020, 10, 11)
                }
            });
            await context.SaveChangesAsync();
            var excepted = 8;

            // �����:
            // ���� 13
            // ����� 10
            // ������ 9
            // ������ 9
            // ���� 8

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
            //
            //SELECT TOP 5
            //G.Id,
            //G.Name,
            //SUM(S.SalesAmount) AS TotalSales
            //FROM
            //    Goods G
            //    JOIN
            //Sales S ON G.Id = S.GoodId
            //GROUP BY
            //G.Id, G.Name, G.Description, G.Category, G.Weight, G.Price
            //    ORDER BY
            //TotalSales DESC;
        }


        /// <summary>
        /// ��� ������ ��������� ������ ������� ����� �������
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task TASK3()
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
                Price = 20,
                Category = "���������", 
                Weight = 10
            };

            var paste = new Good()
            {
                Name = "�����",
                Price = 10,
                Category = "������"
            };

            var pomade = new Good()
            {
                Name = "������",
                Price = 3,
                Category = "���������"
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
            var excepted = 8;

            // �����:
            // ���� 13
            // ����� 10
            // ������ 9
            // ������ 9
            // ���� 8

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
            //
            //SELECT TOP 5
            //G.Id,
            //G.Name,
            //SUM(S.SalesAmount) AS TotalSales
            //FROM
            //    Goods G
            //    JOIN
            //Sales S ON G.Id = S.GoodId
            //GROUP BY
            //G.Id, G.Name, G.Description, G.Category, G.Weight, G.Price
            //    ORDER BY
            //TotalSales DESC;
            //
            //3 ������
            //
            //SELECT a.Category, a.Weight, a.Name
            //    FROM Goods a
            //INNER JOIN(
            //    SELECT Category, MAX(Weight) Weight

            //        FROM Goods

            //        group by Category
            //) b ON a.Category = b.Category AND a.Weight = b.Weight
        }
    }
}