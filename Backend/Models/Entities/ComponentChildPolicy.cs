using System.Data;

namespace Backend.Models.Entities;

public class ComponentChildPolicy : IIntoDto<Dto.ComponentChildPolicy>
{
  public int Id { get; set; }
  public required Component Parent { get; set; }
  public required Component Child { get; set; }

  // Policy backing fields.
  private int? _default;
  private int? _min;
  private int? _max;

  public int Default
  {
    get => _default ?? 0;
    set => _default = value;
  }
  public int Min
  {
    get => _min ?? 0;
    set => _min = value;
  }
  public int Max
  {
    get => _max ?? _default ?? throw new ConstraintException(
      "Component child policy must provide either Default or Max.");
    set => _max = value;
  }

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
