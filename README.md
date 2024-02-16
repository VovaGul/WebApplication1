# WebApplication1

1 запрос. Вывести количество продаж товаров за 10.10.2010, которые стоят больше 10 долларов

```
select Name, SalesAmount, Price
from Goods a
inner join(
	select GoodId, sum(SalesAmount) SalesAmount from Sales
	where SaleDate = '2020-10-10'
	group by GoodId
) b on a.GoodId = b.GoodId
where price > 10
```

2 запрос. Вывести топ 5 самых продаваемых товаров

```
select top 5 Name, SalesAmount
from Goods a
inner join(
	select GoodId, sum(SalesAmount) SalesAmount from Sales
	group by GoodId
) b on a.GoodId = b.GoodId
ORDER BY SalesAmount DESC
```

3 запрос.  Для каждой категории товара вывести самый тяжелый

```
SELECT a.Category, a.Weight, a.Name
    FROM Goods a
INNER JOIN(
    SELECT Category, MAX(Weight) Weight
        FROM Goods
        group by Category
) b ON a.Category = b.Category AND a.Weight = b.Weight
```

4 запрос. Вывести среднюю цену среди категорий товаров, проданную за сегодня и более тысячи штук товаров

```
select Category, avg(Price) Price, sum(SalesAmount) SalesAmount from Goods a
inner join(
	select GoodId, sum(SalesAmount) SalesAmount from Sales
		where SaleDate = CAST(GETDATE() AS DATE)
		group by GoodId
) b on a.GoodId = b.GoodId
group by Category
having sum(SalesAmount) > 1000
```

