namespace Backend.Models.Dto;

public class OrderComponent
{
  public int Id { get; set; }
  public required Component Component { get; set; }
  public required int? Parent { get; set; }
  public required IEnumerable<int> Children { get; set; }
}
