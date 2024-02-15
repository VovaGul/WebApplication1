# WebApplication1

1 запрос. ¬ывести количество продаж товаров за 10.10.2010, которые сто€т больше 10 долларов

```
select Name, SalesAmount, Price
from Goods a
inner join(
	select GoodId, sum(SalesAmount) SalesAmount from Sales
	where CAST(SaleDate AS DATE) = CAST('2020-10-10' AS DATE)
	group by GoodId
) b on a.GoodId = b.GoodId
where price > 10
```

2 запрос. ¬ывести топ 5 самых продаваемых товаров

```
select top 5 Name, SalesAmount
from Goods a
inner join(
	select GoodId, sum(SalesAmount) SalesAmount from Sales
	group by GoodId
) b on a.GoodId = b.GoodId
ORDER BY SalesAmount DESC
```

3 запрос.  ƒл€ каждой категории товара вывести самый т€желый

```
SELECT a.Category, a.Weight, a.Name
    FROM Goods a
INNER JOIN(
    SELECT Category, MAX(Weight) Weight
        FROM Goods
        group by Category
) b ON a.Category = b.Category AND a.Weight = b.Weight
```

4 запрос. ¬ывести среднюю цену среди категорий товаров, проданную за сегодн€ и более тыс€чи штук товаров

```
select Category, avg(Price) Price, sum(SalesAmount) SalesAmount from Goods a
inner join(
	select GoodId, sum(SalesAmount) SalesAmount from Sales
		where CAST(SaleDate AS DATE) = CAST(GETDATE() AS DATE)
		group by GoodId
) b on a.GoodId = b.GoodId
group by Category
having sum(SalesAmount) > 1000
```

