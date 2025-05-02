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

				var actualGroups = actualChildComponents.GroupBy(c => c.Id).ToDictionary(g => g.Key, g => g.Count());
				var standardGroups = standardChildComponents.GroupBy(c => c.Id).ToDictionary(g => g.Key, g => g.Count());

				// Components added more times than standard
				dto.AddedComponents = actualGroups
					.SelectMany(kvp =>
					{
						var standardCount = standardGroups.TryGetValue(kvp.Key, out var sc) ? sc : 0;
						var addedCount = kvp.Value - standardCount;
						return addedCount > 0
							? Enumerable.Repeat(actualChildComponents.First(c => c.Id == kvp.Key).ToDto(), addedCount)
							: Enumerable.Empty<Dto.Component>();
					})
					.ToList();

				// Components missing compared to standard
				dto.RemovedComponents = standardGroups
					.SelectMany(kvp =>
					{
						var actualCount = actualGroups.TryGetValue(kvp.Key, out var ac) ? ac : 0;
						var removedCount = kvp.Value - actualCount;
						return removedCount > 0
							? Enumerable.Repeat(standardChildComponents.First(c => c.Id == kvp.Key).ToDto(), removedCount)
							: Enumerable.Empty<Dto.Component>();
					})
					.ToList();
			}

		}

		return dto;
    }
}
