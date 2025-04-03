namespace Backend.Models.Entities;

public enum OrderStatus
{
  Pending,
  Preparing,
  Done
}

public class Order
{
  public int Id { get; set; }
  public required OrderStatus Status { get; set; }
  public ICollection<OrderComponent> OrderComponents { get; set; } = [];
  public bool TakeAway { get; set; }
}
