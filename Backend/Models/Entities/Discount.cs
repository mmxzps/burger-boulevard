using Microsoft.EntityFrameworkCore;

namespace Backend.Models.Entities;

public class Discount
{
  public int Id { get; set; }
  public decimal Multiplier { get; set; }
}
