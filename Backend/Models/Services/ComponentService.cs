namespace Backend.Models.Services
{
	public class ComponentService
	{
	
		public Dto.Component ToComponentDto(Entities.Component component) => new Dto.Component
		{
			Id = component.Id,
			Level = component.Level,
			Name = component.Name,
			Description = component.Description,
			ImageUrl = component.ImageUrl,
			Categories = component.Categories.Select(ToCategoryDto),
			Allergens = component.Allergens.Select(ToAllergenDto),
			ChildPolicies = component.ChildPolicies.Select(ToChildPolicyDto),
			OriginalPrice = component.Price,
			VatRate = component.Vat,
			Discount = component.Discount?.Multiplier
		};
		public Dto.FeaturedComponent ToFeaturedComponentDto(Entities.FeaturedComponent featuredComponent) => new Dto.FeaturedComponent
		{
			Id = featuredComponent.Id,
			Title = featuredComponent.Title,
			Component = ToComponentDto(featuredComponent.Component)
		};
		public Dto.ComponentChildPolicy ToChildPolicyDto(Entities.ComponentChildPolicy childPolicy) => new Dto.ComponentChildPolicy
		{
			Id = childPolicy.Id,
			Child = ToComponentDto(childPolicy.Child),
			Default = childPolicy.Default,
			Min = childPolicy.Min,
			Max = childPolicy.Max
		};
		public Dto.Category ToCategoryDto(Entities.Category category) => new Dto.Category
		{
			Id = category.Id,
			Name = category.Name,
			ImageUrl = category.ImageUrl
		};
		public Dto.Allergen ToAllergenDto(Entities.Allergen allergen) => new Dto.Allergen
		{
			Id = allergen.Id,
			Name = allergen.Name
		};
	}
}
