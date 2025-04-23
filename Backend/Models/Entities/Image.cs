using Microsoft.EntityFrameworkCore;

namespace Backend.Models.Entities;

public class Image
{
  public int Id { get; set; }
  [Comment("PNG image data")]
  public required byte[] Data { get; set; }
  public virtual ICollection<Component> Components { get; set; } = [];
}
