namespace Backend.Models.Dto;

public class Order
{
    public int Id { get; set; }
    public required Entities.OrderStatus Status { get; set; }
    public required IEnumerable<OrderComponent> Components { get; set; }
    public bool TakeAway { get; set; }
    public decimal TotalPrice { get; set; }
}
