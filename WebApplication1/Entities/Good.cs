namespace WebApplication1.Entities;

public class Good
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public string Category { get; set; } = String.Empty;
    public string Weight { get; set; } = String.Empty;
    public int Price { get; set; }
}