using System.Data;

namespace Backend.Models.Entities;

public class ComponentChildPolicy : IntoDto<Dto.ComponentChildPolicy>
{
  public int Id { get; set; }
  public required Component Parent { get; set; }
  public required Component Child { get; set; }

  // Policy backing fields.
  private int? _default;
  private int? _min;
  private int? _max;

  public int Default => _default ?? 0;
  public int Min => _min ?? 0;
  public int Max => _max ?? _default ?? throw new ConstraintException(
      "Component child policy must provide either Default or Max.");

  public Dto.ComponentChildPolicy ToDto() => new Dto.ComponentChildPolicy
  {
    Id = Id,
    Parent = Parent.ToDto(),
    Child = Child.ToDto(),
    Default = Default,
    Min = Default,
    Max = Default
  };
}
