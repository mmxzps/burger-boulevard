namespace Backend.Models.Entities;

public class Price
{
  public int Id { get; set; }
  public decimal BasePrice { get; set; }
  public required Discount Discount { get; set; }
}
