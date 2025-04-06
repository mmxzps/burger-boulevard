using Backend.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend;

public class BackendContext : DbContext
{
	public required DbSet<Component> Components { get; set; }
	public required DbSet<ComponentChildPolicy> ComponentChildPolicies { get; set; }
	public required DbSet<Category> Categories { get; set; }
	public required DbSet<Price> Prices { get; set; }
	public required DbSet<Discount> Discounts { get; set; }
	public required DbSet<FeaturedComponent> FeaturedComponents { get; set; }
	public required DbSet<Order> Orders { get; set; }
	public required DbSet<OrderComponent> OrderComponents { get; set; }

	public BackendContext(DbContextOptions<BackendContext> options) : base(options) { }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<ComponentChildPolicy>()
      .HasOne(p => p.Parent)
      .WithMany(c => c.ChildPolicies)
      .OnDelete(DeleteBehavior.Restrict);
	}
}
