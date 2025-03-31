using Microsoft.EntityFrameworkCore;

namespace Backend.Models.Entities;

public class Discount
{
  public int Id { get; set; }
  [Precision(8, 4)]
  public decimal Multiplier { get; set; }
}
