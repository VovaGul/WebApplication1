namespace WebApplication1.Entities;

public class Good
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public string Category { get; set; } = String.Empty;
    public int Weight { get; set; }
    public int Price { get; set; }
}