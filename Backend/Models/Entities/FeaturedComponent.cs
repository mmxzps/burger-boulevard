namespace Backend.Models.Entities;

public class FeaturedComponent : IIntoDto<Dto.FeaturedComponent>
{
  public int Id { get; set; }
  public required string Title { get; set; }
  public required Component Component { get; set; }

  public Dto.FeaturedComponent ToDto() => new Dto.FeaturedComponent
  {
    Id = Id,
    Title = Title,
    Component = Component.ToDto()
  };
}
