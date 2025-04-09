namespace Backend.Models.Dto.Create;

public class Order
{
  /// <summary>
  /// Non-flat, recursive list of components.
  /// </summary>
  public required IEnumerable<OrderComponent> Components { get; set; }
  public bool TakeAway { get; set; }
}
