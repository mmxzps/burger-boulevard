namespace Backend.Models.Entities
{
	public class BaseProductIngredient
	{
		public int Id { get; set; }
		public required Product Product { get; set; }
		public required Ingredient Ingredient { get; set; }
		public bool Removable { get; set; }
	}
}
