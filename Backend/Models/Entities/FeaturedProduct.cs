namespace Backend.Models.Entities
{
	public class FeaturedProduct
	{
		public int Id { get; set; }
		public required string Title { get; set; }
		public required Product Product { get; set; }
	}
}
