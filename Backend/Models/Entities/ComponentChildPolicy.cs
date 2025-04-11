namespace Backend.Models.Entities;

public class ComponentChildPolicy : IIntoDto<Dto.ComponentChildPolicy>
{
  public int Id { get; set; }
  public required Component Parent { get; set; }
  public required Component Child { get; set; }

  public int Default { get; set; }
  public int Min { get; set; }
  public int Max { get; set; }

  public Dto.ComponentChildPolicy ToDto() => new Dto.ComponentChildPolicy
  {
    Id = Id,
    Child = Child.ToDto(),
    Default = Default,
    Min = Min,
    Max = Max
  };
}
