namespace Backend.Models.Entities;

public class OrderProduct
{
  public int Id { get; set; }
  public required Product Product { get; set; }
  public required Order Order { get; set; }
  public required ICollection<ModifiedOrderProductIngredient> ModifiedOrderProductIngredients { get; set; } = [];
}
