namespace Backend.Models.Entities;

public enum OrderStatus
{
  Pending,
  Preparing,
  Done
}

public class Order : IIntoDto<Dto.Order>
{
  public int Id { get; set; }
  public required OrderStatus Status { get; set; }
  public virtual ICollection<OrderComponent> Components { get; set; } = [];
  public bool TakeAway { get; set; }
  public TimeOnly OrderTime { get; set; } = TimeOnly.FromDateTime(DateTime.Now);

	public Dto.Order ToDto() => new Dto.Order
  {
    Id = Id,
    Status = Status,
    Components = Components.Select(c => c.ToDto()),
    TakeAway = TakeAway
  };
}
