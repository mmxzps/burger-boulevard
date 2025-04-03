namespace Backend.Models.Entities;

public class ProductIngredient
{
  public int Id { get; set; }
  public required Product Product { get; set; }
  public required Ingredient Ingredient { get; set; }
  public uint BaseAmount { get; set; }
  public uint MinAmount { get; set; }
  public uint MaxAmount { get; set; }
}
