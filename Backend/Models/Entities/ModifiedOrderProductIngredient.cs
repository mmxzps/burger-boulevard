namespace Backend.Models.Entities
{
	public class ModifiedOrderProductIngredient
	{
		public int Id { get; set; }
		public required OrderProduct OrderProduct { get; set; }
		public required Ingredient Ingredient { get; set; }
		public bool Added { get; set; }

	}
}
