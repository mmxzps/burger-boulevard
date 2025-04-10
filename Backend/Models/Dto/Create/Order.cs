namespace Backend.Models.Dto.Create;

public class Order
{
  /// <summary>
  /// Non-flat, recursive list of components.
  /// </summary>
  public required IEnumerable<OrderComponent> Components { get; set; }
  public bool TakeAway { get; set; }

  public ICollection<Entities.OrderComponent> ToOrderComponentEntities(BackendContext context, Entities.Order order) =>
    Components.Select(c => c.ToOrderComponentEntity(context, order, null)).ToList();
}
