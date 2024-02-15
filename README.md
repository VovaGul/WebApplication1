# WebApplication1

1 ������

```
SELECT Sum(SalesAmount) FROM Sales
INNER JOIN Goods ON Sales.GoodId = Goods.Id
Where SaleDate = '2020-10-10' and price > 10
```

2 ������ 

```
SELECT TOP 5
G.Id,
G.Name,
SUM(S.SalesAmount) AS TotalSales
FROM
    Goods G
    JOIN
Sales S ON G.Id = S.GoodId
GROUP BY
G.Id, G.Name, G.Description, G.Category, G.Weight, G.Price
    ORDER BY
TotalSales DESC;
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

