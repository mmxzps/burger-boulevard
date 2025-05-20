namespace Backend.Models.Entities;

public class FeaturedComponent
{
  public int Id { get; set; }
  public required string Title { get; set; }
  public required Component Component { get; set; }

}
