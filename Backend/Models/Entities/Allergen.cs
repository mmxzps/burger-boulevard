namespace Backend.Models.Entities;

public class Allergen : IIntoDto<Dto.Allergen>
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public virtual ICollection<Component> Components { get; set; } = [];

    public Dto.Allergen ToDto() => new Dto.Allergen
    {
        Id = Id,
        Name = Name
    };
}
