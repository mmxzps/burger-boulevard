using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models.Entities;

public enum ComponentLevel
{
  Ingredient = 0,
  Product = 1,
  Menu = 2
}

public class Component : IntoDto<Dto.Component>
{
  public int Id { get; set; }
  public ComponentLevel Level { get; set; }
  public required string Name { get; set; }
	public required string? Description { get; set; }
	public ICollection<Category> Categories { get; set; } = [];

  [InverseProperty("Parent")]
  public virtual ICollection<ComponentChildPolicy> ChildPolicies { get; set; } = [];

  public virtual ICollection<OrderComponent> OrderComponents { get; set; } = [];

  public required Price Price { get; set; }
	public required Discount? Discount { get; set; }

  private bool? _vegan;

  public bool Vegan
  {
    get => _vegan ?? ChildPolicies.Any(p => p.Child.Vegan);
    set => _vegan = value;
  }
  public int? DisplayOrderIndex { get; set; } // Only useful for level 0 components.

  public Dto.Component ToDto() => new Dto.Component
  {
    Id = Id,
    Level = Level,
    Name = Name,
    Description = Description,
    Categories = Categories.Select(c => c.ToDto()),
    Price = Price.BasePrice,
    Discount = Discount?.Multiplier,
    Vegan = Vegan,
    DisplayOrderIndex = DisplayOrderIndex
  };
}
