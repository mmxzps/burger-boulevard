namespace Backend.Models.Entities;

public class OrderComponent
{
  public int Id { get; set; }
  public required Order Order { get; set; }

  public required Component Component { get; set; }
  public required OrderComponent? Parent { get; set; }
  public ICollection<OrderComponent> Children { get; set; } = [];
}
