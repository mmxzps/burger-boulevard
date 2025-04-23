namespace Backend.Models.Dto;

public class ComponentChildPolicy
{
  public int Id { get; set; }
  public required Component Child { get; set; }

  public int Default { get; set; }
  public int Min { get; set; }
  public int Max { get; set; }
}
