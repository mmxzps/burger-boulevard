namespace Backend.Models.Entities;

public class Ingredient
{
  public int Id { get; set; }
  public required string Name { get; set; }
  public required Price Price { get; set; }
  public bool Vegan { get; set; }
  public int DisplayOrderIndex { get; set; }
  public required ICollection<ProductIngredient> ProductIngredients { get; set; } // = new()?
  public required ICollection<ModifiedOrderProductIngredient> ModifiedOrderProductIngredients { get; set; } //Tillagd  = new()?
}
