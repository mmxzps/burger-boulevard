namespace Backend.Models.Entities;

public enum ProductModification
{
  Add,
  Remove
}

public class ModifiedOrderProductIngredient
{
  public int Id { get; set; }
  public required Ingredient Ingredient { get; set; }
  public required ProductModification Modification { get; set; }
}
