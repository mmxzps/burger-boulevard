namespace Backend.Models.Entities;

public class ComponentChild
{
  public int Id { get; set; }
  public required Component Parent { get; set; }
  public required Component Child { get; set; }
  public int BaseAmount { get; set; }
  public int MinAmount { get; set; }
  public int MaxAmount { get; set; }
}
