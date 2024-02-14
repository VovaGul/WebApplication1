namespace CRUD.Entities;

public class Sale
{
    public int Id { get; set; }
    public Good Good { get; set; }
    public int SalesAmount { get; set; }
    public DateTime SaleDate {get; set; }
}