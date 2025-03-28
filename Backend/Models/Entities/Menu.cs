namespace Backend.Models.Entities;

public class Menu
{
  public int Id { get; set; }
  public required string Name { get; set; }
  public required List<Product> Products { get; set; }
  public required Category Category { get; set; }
  public required Price Price { get; set; }
}
