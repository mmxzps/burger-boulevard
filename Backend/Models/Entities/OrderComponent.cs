namespace Backend.Models.Entities;

public class OrderComponent
{
	public int Id { get; set; }
	public required Order Order { get; set; }

	public required Component Component { get; set; }
	public int? ParentId { get; set; }
	public required OrderComponent? Parent { get; set; }
	public virtual ICollection<OrderComponent> Children { get; set; } = [];

	public decimal EvaluatePrice()
	{
		var price = Component.CurrentPrice;

		/*
          A component and its children altogether use the parent's cost,
          unless the component amounts deviate from their defaults.
          Amounts less than default cost the same,
          but amounts greater than default cost as much as that child component does,
          multiplied by the difference between the amount and the default.
        */
		// TODO: Fix bug where the price is not calculated correctly, and this appears to be zero.
		var additionalPrice = Children
		  .GroupBy(oc => oc.Component.Id)
		  .Sum(g =>
			  Math.Max(g.Count() - Component.ChildPolicies.First(p => p.Child.Id == g.Key).Default, 0)
			  * g.First().EvaluatePrice());

		return price + additionalPrice;
	}

	public void VerifyPolicies()
	{
		// Ensure all policies are followed.
		foreach (var policy in Component.ChildPolicies)
		{
			var amount = Children.Count(oc => oc.Component.Id == policy.Child.Id);
			if (amount < policy.Min || amount > policy.Max)
				throw new Exception(
					$"Policy broken: invalid amount of '{policy.Child.Name}' (ID = {policy.Child.Id}) children for '{Component.Name}' (ID = {Component.Id}) component (got {amount}, min {policy.Min}, max {policy.Max}).");
		}

		foreach (var child in Children)
		{
			// Ensure no additional components are present,
			// by checking if any child has a component ID that is not inside a policy.
			if (!Component.ChildPolicies.Any(p => p.Child.Id == child.Component.Id))
				throw new Exception(
					$"Policy broken: unexpected child '{child.Component.Name}' (ID = {child.Component.Id}) found for '{Component.Name}' (ID = {Component.Id}) component.");

			// Recursive check.
			child.VerifyPolicies();
		}
	}
	
}


