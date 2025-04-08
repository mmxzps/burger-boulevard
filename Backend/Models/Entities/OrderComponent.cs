namespace Backend.Models.Entities;

public class OrderComponent : IIntoDto<Dto.OrderComponent>
{
  public int Id { get; set; }
  public required Order Order { get; set; }

  public required Component Component { get; set; }
  public required OrderComponent? Parent { get; set; }
  public virtual ICollection<OrderComponent> Children { get; set; } = [];

  public Dto.OrderComponent ToDto() => new Dto.OrderComponent
  {
    Id = Id,
    Component = Component.ToDto(),
    Parent = Parent?.Id,
    Children = Children.Select(c => c.Id)
  };
}
