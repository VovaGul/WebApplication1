# WebApplication1

1 ������

```
SELECT Sum(SalesAmount) FROM Sales
INNER JOIN Goods ON Sales.GoodId = Goods.Id
Where SaleDate = '2020-10-10' and price > 10
```

2 ������ 

```
select top 5 Name, SalesAmount
from Goods a
inner join(
	select GoodId, sum(SalesAmount) SalesAmount from Sales
	group by GoodId
) b on a.Id = b.GoodId
ORDER BY SalesAmount DESC
```

3 ������

```
SELECT a.Category, a.Weight, a.Name
    FROM Goods a
INNER JOIN(
    SELECT Category, MAX(Weight) Weight
        FROM Goods
        group by Category
) b ON a.Category = b.Category AND a.Weight = b.Weight
```

4 ������ 

```
```

