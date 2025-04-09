using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models.Entities;

public enum ComponentLevel
{
  Ingredient = 0,
  Product = 1,
  Menu = 2
}

public class Component : IIntoDto<Dto.Component>
{
  public int Id { get; set; }
  public ComponentLevel Level { get; set; }
  public required string Name { get; set; }
	public string? Description { get; set; }
  public int? ImageId { get; set; }
  public required Image? Image { get; set; }
  public string? ImageUrl =>
    ImageId is int id ? $"/Images/{id}" : null;
	public virtual ICollection<Category> Categories { get; set; } = [];

  public virtual ICollection<ComponentChildPolicy> ChildPolicies { get; set; } = [];

  public virtual ICollection<OrderComponent> OrderComponents { get; set; } = [];

  public required Price Price { get; set; }
	public required Discount? Discount { get; set; }

  public int? DisplayOrderIndex { get; set; } // Only useful for level 0 components.

  public Dto.Component ToDto() => new Dto.Component
  {
    Id = Id,
    Level = Level,
    Name = Name,
    Description = Description,
    ImageUrl = ImageUrl,
    Categories = Categories.Select(c => c.ToDto()),
    Price = Price.BasePrice,
    Discount = Discount?.Multiplier,
    DisplayOrderIndex = DisplayOrderIndex
  };
}
