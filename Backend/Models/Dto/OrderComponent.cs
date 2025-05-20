namespace Backend.Models.Dto;

public class OrderComponent
{
    public int Id { get; set; }
    public required Component Component { get; set; }
    public required List<Component> AddedComponents { get; set; }
    public required List<Component> RemovedComponents { get; set; }
	public required int? Parent { get; set; }
    public decimal TotalPrice { get; set; }
}
