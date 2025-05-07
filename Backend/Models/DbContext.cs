using Backend.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend;

public class BackendContext : DbContext
{
    public required DbSet<Component> Components { get; set; }
    public required DbSet<ComponentChildPolicy> ComponentChildPolicies { get; set; }
    public required DbSet<Category> Categories { get; set; }
    public required DbSet<Allergen> Allergens { get; set; }
    public required DbSet<Discount> Discounts { get; set; }
    public required DbSet<FeaturedComponent> FeaturedComponents { get; set; }
    public required DbSet<Order> Orders { get; set; }
    public required DbSet<OrderComponent> OrderComponents { get; set; }
    public required DbSet<Image> Images { get; set; }

    public BackendContext(DbContextOptions<BackendContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ComponentChildPolicy>(b =>
            {
                b.HasOne(p => p.Parent)
                  .WithMany(c => c.ChildPolicies)
                  .OnDelete(DeleteBehavior.Restrict);
                b.Navigation(p => p.Child).AutoInclude();
            });

        modelBuilder.Entity<Component>(b =>
            {
                b.Navigation(c => c.Allergens).AutoInclude();
                b.Navigation(c => c.Categories).AutoInclude();
                b.Navigation(c => c.Discount).AutoInclude();
            });
	    modelBuilder.Entity<Component>()
		    .Property(c => c.Vat)
		    .HasColumnType("decimal(8, 4)");  // You can adjust precision and scale as necessary
    }
}
