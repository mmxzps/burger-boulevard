namespace Backend.Models.Entities
{
	public class MenuProduct
	{
		public int Id { get; set; }
		public required Menu Menu { get; set; }
		public required Product Product { get; set; }
	}
}
