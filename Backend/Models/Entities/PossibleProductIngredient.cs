namespace Backend.Models.Entities;

public class PossibleProductIngredient
{
  public int Id { get; set; }
  public required Product Product { get; set; }
  public required Ingredient Ingredient { get; set; }
  public uint MaxAmount { get; set; }
}
