﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApplication1;
using WebApplication1.Controllers;
using WebApplication1.Entities;

namespace TestProject1;

public class GoodsTests
{
    [Fact]
    public async Task Create_good()
    {
    }

    [Fact]
    public async Task Read_all_goods()
    {
        // Arrange
        var serviceProvider = new ServiceCollection()
            .AddSingleton(_ => new Context(new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options))
            .AddSingleton<GoodsController>()
            .AddSingleton<ComparerAsValueObjects>()
            .BuildServiceProvider();

        var expected = new List<Good>
        {
            new() { GoodId = 1, Name = "TestGood1" },
            new() { GoodId = 2, Name = "TestGood2" }
        };

        await using var context = serviceProvider.GetRequiredService<Context>();
        await context.Goods.AddRangeAsync(new List<Good>
        {
            new() { Name = "TestGood1" },
            new() { Name = "TestGood2" }
        });
        await context.SaveChangesAsync();

        //Act
        var result = await serviceProvider.GetRequiredService<GoodsController>().GetGoods();

        // Assert
        Assert.Equal(expected, result.Value, 
            serviceProvider.GetRequiredService<ComparerAsValueObjects>());
    }

    [Fact]
    public async Task Read_good()
    {
        // Arrange
        var serviceProvider = new ServiceCollection()
            .AddSingleton(_ => new Context(new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options))
            .AddSingleton<GoodsController>()
            .AddSingleton<ComparerAsValueObjects>()
            .BuildServiceProvider();

        var expected = new Good { GoodId = 1, Name = "TestGood1" };

        await using var context = serviceProvider.GetRequiredService<Context>();
        await context.Goods.AddRangeAsync(new List<Good>
        {
            new() { Name = "TestGood1" },
            new() { Name = "TestGood2" }
        });
        await context.SaveChangesAsync();

        //Act
        var result = await serviceProvider.GetRequiredService<GoodsController>().GetGood(1);

        // Assert
        Assert.Equal(expected, result.Value,
            serviceProvider.GetRequiredService<ComparerAsValueObjects>());
    }

    [Fact]
    public async Task Read_not_exist_good()
    {
        // Arrange
        var serviceProvider = new ServiceCollection()
            .AddSingleton(_ => new Context(new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options))
            .AddSingleton<GoodsController>()
            .AddSingleton<ComparerAsValueObjects>()
            .BuildServiceProvider();

        await using var context = serviceProvider.GetRequiredService<Context>();
        await context.Goods.AddRangeAsync(new List<Good>
        {
            new() { Name = "TestGood1" },
            new() { Name = "TestGood2" }
        });
        await context.SaveChangesAsync();

        //Act
        var result = await serviceProvider.GetRequiredService<GoodsController>().GetGood(4);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);
        Assert.Equal(404, notFoundResult.StatusCode);
    }

    [Fact]
    public async Task Update_good()
    {
    }

    [Fact]
    public async Task Update_not_exist_good()
    {
    }

    [Fact]
    public async Task Delete_good()
    {
    }

    [Fact]
    public async Task Delete_not_exist_good()
    {
    }
}