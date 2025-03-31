using Microsoft.EntityFrameworkCore;

namespace Backend.Models.Entities;

public class Price
{
  public int Id { get; set; }
  [Precision(8, 4)]
  public decimal BasePrice { get; set; }
  public required Discount Discount { get; set; }
}
