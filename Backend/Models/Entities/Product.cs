namespace Backend.Models.Entities;

public class Product
{
  public int Id { get; set; }
  public required string Name { get; set; }
  public required string Description { get; set; }
  public required Price Price { get; set; }
  public required Category Category { get; set; }
  public required List<BaseProductIngredient> BaseProductIngredients { get; set; }
  public required List<PossibleProductIngredient> PossibleProductIngredients { get; set; }
  public required List<OrderProduct> OrderProducts { get; set; }
}
