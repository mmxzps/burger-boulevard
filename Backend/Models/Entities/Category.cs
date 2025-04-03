namespace Backend.Models.Entities;

public class Category
{
  public int Id { get; set; }
  public required string Name { get; set; }
  public required ICollection<Product> Products { get; set; } = [];
}
