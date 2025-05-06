namespace Backend.Models.Entities;

public class Category : IIntoDto<Dto.Category>
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int? ImageId { get; set; }
    public required Image? Image { get; set; }
    public string? ImageUrl =>
      ImageId is int id ? $"/api/Images/{id}" : null;
    public virtual ICollection<Component> Components { get; set; } = [];

    public Dto.Category ToDto() => new Dto.Category
    {
        Id = Id,
        Name = Name,
        ImageUrl = ImageUrl
    };
}
