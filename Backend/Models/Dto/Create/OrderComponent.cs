namespace Backend.Models.Dto.Create;

public class OrderComponent
{
  public required int ComponentId { get; set; }
  public required IEnumerable<OrderComponent> Children { get; set; }

  public Models.Entities.OrderComponent ToOrderComponentEntity(
      BackendContext context,
      Entities.Order order,
      Entities.OrderComponent? parent) =>
    new Entities.OrderComponent
    {
      Order = order,
      Component = context.Components.Find(ComponentId)
        ?? throw new InvalidOperationException($"Invalid component ID: {ComponentId}."),
      Parent = parent,
      Children = []
    };

  public ICollection<Entities.OrderComponent> ToFlatCrossReferencingOrderComponents(
      BackendContext context,
      Entities.Order order,
      Entities.OrderComponent? parent)
  {
     var thisComponent = ToOrderComponentEntity(context, order, parent);
     var descendantComponents = Children.SelectMany(oc =>
         oc.ToFlatCrossReferencingOrderComponents(context, order, thisComponent)).ToList();
     thisComponent.Children = descendantComponents.Where(oc => oc.Parent == thisComponent).ToList();

     var flatComponents = new List<Entities.OrderComponent>(descendantComponents);

     if (parent is null)
       flatComponents.Add(thisComponent);

     return flatComponents;
  }
}
