using System.Text.Json.Serialization;

namespace Backend.Models.Dto;

public record Price(decimal Current, decimal Original, decimal? Discount, decimal VatAmount, decimal VatRate);

public class Component
{
    public int Id { get; set; }
    public Entities.ComponentLevel Level { get; set; }
    public required string Name { get; set; }
    public required string? Description { get; set; }
    public required IEnumerable<ComponentChildPolicy> ChildPolicies { get; set; }
    public required IEnumerable<Category> Categories { get; set; }
    public required IEnumerable<Allergen> Allergens { get; set; }
    public string? ImageUrl { get; set; }

    public Price Price => new(
	    Current: CalculatePriceAfterDiscount(OriginalPrice, Discount),
	    Original: OriginalPrice,
	    Discount: Discount,
	    VatAmount: CalculateVatAmount(CalculatePriceAfterDiscount(OriginalPrice, Discount), VatRate),
		VatRate: VatRate
    );
	[JsonIgnore] public decimal OriginalPrice { get; set; }
    [JsonIgnore] public decimal? Discount { get; set; }
	[JsonIgnore] public decimal VatRate { get; set; }


	public bool Independent { get; set; }

	private decimal CalculatePriceAfterDiscount(decimal originalPrice, decimal? discount)
	{
		return originalPrice * (discount ?? 1);
	}
	private decimal CalculateVatAmount(decimal priceWithDiscount, decimal vatRate)
	{
		return priceWithDiscount - (priceWithDiscount / (1 + vatRate));
	}
}
