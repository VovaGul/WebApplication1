# WebApplication1

1 запрос

```
select Name, SalesAmount, Price
from Goods a
inner join(
	select GoodId, sum(SalesAmount) SalesAmount from Sales
	where SaleDate = '2020-10-10'
	group by GoodId
) b on a.Id = b.GoodId
where price > 10
```

2 запрос 

```
select top 5 Name, SalesAmount
from Goods a
inner join(
	select GoodId, sum(SalesAmount) SalesAmount from Sales
	group by GoodId
) b on a.Id = b.GoodId
ORDER BY SalesAmount DESC
```

3 запрос

```
SELECT a.Category, a.Weight, a.Name
    FROM Goods a
INNER JOIN(
    SELECT Category, MAX(Weight) Weight
        FROM Goods
        group by Category
) b ON a.Category = b.Category AND a.Weight = b.Weight
```

4 запрос 

```
select Category, avg(Price) Price, sum(SalesAmount) SalesAmount from Goods a
inner join(
	select GoodId, sum(SalesAmount) SalesAmount from Sales
		where CAST(SaleDate AS DATE) = CAST(GETDATE() AS DATE)
		group by GoodId
) b on a.Id = b.GoodId
group by Category
having sum(SalesAmount) > 1000
```

