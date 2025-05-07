using Microsoft.EntityFrameworkCore;

namespace Backend.Models.Entities;

public enum ComponentLevel
{
    Ingredient = 0,
    Product = 1,
    Menu = 2
}

public class Component
{
    public int Id { get; set; }
    public ComponentLevel Level { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public int? ImageId { get; set; }
    public required Image? Image { get; set; }
    public string? ImageUrl =>
      ImageId is int id ? $"/api/Images/{id}" : null;
    public virtual ICollection<Category> Categories { get; set; } = [];
    public virtual ICollection<Allergen> Allergens { get; set; } = [];

    public virtual ICollection<ComponentChildPolicy> ChildPolicies { get; set; } = [];

    public virtual ICollection<OrderComponent> OrderComponents { get; set; } = [];

    public decimal CurrentPrice =>
      Price * (Discount?.Multiplier ?? 1);

    [Precision(8, 4)]
    public decimal Price { get; set; }
    public required Discount? Discount { get; set; }

    public bool Independent { get; set; }

    
}
