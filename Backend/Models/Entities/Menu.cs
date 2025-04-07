namespace Backend.Models.Entities;

public class Menu
{
	public int Id { get; set; }
	public required string Name { get; set; }
	public required ICollection<MenuProduct> MenuProducts { get; set; } = [];
	public required Category Category { get; set; }
	public required Price Price { get; set; }
	public required Discount? Discount { get; set; }
}
