using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Entities;

public class Good
{
    public int GoodId { get; set; }
    [MaxLength(100)]
    public string? Name { get; set; }
    [MaxLength(1000)]
    public string? Description { get; set; }
    [MaxLength(100)]
    public string? Category { get; set; }
    public int? Weight { get; set; }
    public int? Price { get; set; }
}