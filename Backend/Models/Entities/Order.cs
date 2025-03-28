namespace Backend.Models.Entities
{
	public class Order
	{
		public int Id { get; set; }
		public required OrderStatus Status { get; set; }
		public bool TakeAway { get; set; }
		public required List<OrderProduct> OrderProducts { get; set; }

	}
}
