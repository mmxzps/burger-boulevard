namespace Backend.Models.Entities;

public class Category : IntoDto<Dto.Category>
{
  public int Id { get; set; }
  public required string Name { get; set; }
  public ICollection<Component> Components { get; set; } = [];

  public Dto.Category ToDto() => new Dto.Category()
  {
    Id = Id,
    Name = Name
  };
}
