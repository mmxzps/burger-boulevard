namespace Backend.Models.Entities;

public class ProductIngredient
{
  public int Id { get; set; }
  public uint BaseAmount { get; set; }
  public uint MinAmount { get; set; }
  public uint MaxAmount { get; set; }
}
