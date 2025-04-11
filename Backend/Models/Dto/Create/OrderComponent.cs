namespace Backend.Models.Dto.Create;

public class OrderComponent
{
  public required int ComponentId { get; set; }
  public required IEnumerable<OrderComponent> Children { get; set; }

  public Models.Entities.OrderComponent ToOrderComponentEntity(
      BackendContext context,
      Entities.Order order,
      Entities.OrderComponent? parent)
  {
    var entity = new Entities.OrderComponent
    {
      Order = order,
      Component = context.Components.Find(ComponentId)
        ?? throw new InvalidOperationException($"Invalid component ID: {ComponentId}."),
      Parent = parent,
      Children = []
    };

    entity.Children = Children.Select(oc =>
        oc.ToOrderComponentEntity(context, order, entity)).ToList();

    return entity;
  }
}
