namespace Backend.Models.Dto;

public class Component
{
  public int Id { get; set; }
  public Entities.ComponentLevel Level { get; set; }
  public required string Name { get; set; }
	public required string? Description { get; set; }
	public required IEnumerable<Category> Categories { get; set; }
  public string? ImageUrl { get; set; }

  public decimal Price { get; set; }
	public decimal? Discount { get; set; }

  public bool? Vegan { get; set; }
  public int? DisplayOrderIndex { get; set; }
}
