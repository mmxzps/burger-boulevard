using Microsoft.EntityFrameworkCore;

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

    public Dto.Component ToDto()
    {
      var dto = new Dto.Component
      {
        Id = Id,
        Level = Level,
        Name = Name,
        Description = Description,
        ImageUrl = ImageUrl,
        AddedComponents = new(),
        RemovedComponents = new(),
        Categories = Categories.Select(c => c.ToDto()),
        OriginalPrice = Price,
        Discount = Discount?.Multiplier
      };

      if (OrderComponents.Any())
      {
        var orderComponent = OrderComponents.FirstOrDefault();

        if (orderComponent != null)
        {
          var actualChildComponents = orderComponent
            .Order.Components.Where(c => c.ParentId == orderComponent.Id)
            .Select(c => c.Component)
            .ToList();

          var standardChildComponents = ChildPolicies
            .Select(p => p.Child)
            .ToList();

	        

          dto.AddedComponents = actualChildComponents
            .Where(ac => !standardChildComponents.Any(sc => sc.Id == ac.Id))
            .Select(c => c.ToDto())
            .ToList();

          dto.RemovedComponents = standardChildComponents
            .Where(sc => !actualChildComponents.Any(ac => ac.Id == sc.Id))
            .Select(c => c.ToDto())
            .ToList();
        }

      }

      return dto;
    }
}
