using System.Text.Json.Serialization;

namespace Backend.Models.Dto;

public record Price(decimal Current, decimal Original, decimal? Discount);

public class Component
{
    public int Id { get; set; }
    public Entities.ComponentLevel Level { get; set; }
    public required string Name { get; set; }
    public required string? Description { get; set; }
    public required IEnumerable<Category> Categories { get; set; }
    public string? ImageUrl { get; set; }

    public Price Price => new(OriginalPrice * (Discount ?? 1), OriginalPrice, Discount);

    [JsonIgnore] public decimal OriginalPrice { get; set; }
    [JsonIgnore] public decimal? Discount { get; set; }
}
