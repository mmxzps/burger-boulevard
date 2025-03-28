namespace Backend.Models.Entities;

public class Ingredient
{
  public int Id { get; set; }
  public required string Name { get; set; }
  public required Price Price { get; set; }
  public bool Vegan { get; set; }
  public required List<BaseProductIngredient> BaseProductIngredients { get; set; }
  public required List<PossibleProductIngredient> PossibleProductIngredients { get; set; }
}
