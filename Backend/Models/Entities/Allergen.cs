namespace Backend.Models.Entities;

public class Allergen
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public virtual ICollection<Component> Components { get; set; } = [];

    
}
