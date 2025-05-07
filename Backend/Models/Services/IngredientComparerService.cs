namespace Backend.Models.Services
{
	public class IngredientComparerService
	{
		private readonly ComponentService _componentService;
		

		public IngredientComparerService(ComponentService componentService)
		{
			_componentService = componentService;
		}
		public List<Dto.Component> GetAddedIngredients(Entities.OrderComponent orderComponent)
		{
			var actual = GetActualComponentCounts(orderComponent);
			var standard = GetStandardComponentCounts(orderComponent);

			return actual
				.SelectMany(kvp =>
				{
					var standardCount = standard.TryGetValue(kvp.Key, out var sc) ? sc : 0;
					var addedCount = kvp.Value - standardCount;
					return addedCount > 0
						? Enumerable.Repeat(_componentService.ToComponentDto(orderComponent.Order.Components.First(c => c.Component.Id == kvp.Key).Component), addedCount)
						: Enumerable.Empty<Dto.Component>();
				})
				.ToList();
		}

		public List<Dto.Component> GetRemovedIngredients(Entities.OrderComponent orderComponent)
		{
			var actual = GetActualComponentCounts(orderComponent);
			var standard = GetStandardComponentCounts(orderComponent);

			return standard
				.SelectMany(kvp =>
				{
					var actualCount = actual.TryGetValue(kvp.Key, out var ac) ? ac : 0;
					var removedCount = kvp.Value - actualCount;
					return removedCount > 0
						? Enumerable.Repeat(_componentService.ToComponentDto(orderComponent.Component.ChildPolicies.First(p => p.Child.Id == kvp.Key).Child), removedCount)
						: Enumerable.Empty<Dto.Component>();
				})
				.ToList();
		}
		private Dictionary<int, int> GetActualComponentCounts(Entities.OrderComponent orderComponent)
		{
			return orderComponent.Order.Components
				.Where(c => c.ParentId == orderComponent.Id)
				.GroupBy(c => c.Component.Id)
				.ToDictionary(g => g.Key, g => g.Count());
		}

		private Dictionary<int, int> GetStandardComponentCounts(Entities.OrderComponent orderComponent)
		{
			return orderComponent.Component.ChildPolicies
				.Select(p => p.Child)
				.GroupBy(c => c.Id)
				.ToDictionary(g => g.Key, g => g.Count());
		}
	}
}
