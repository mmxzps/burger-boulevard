using Backend.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend;

public class BackendContext : DbContext
{
	public required DbSet<Component> Components { get; set; }
	public required DbSet<ComponentChild> ComponentChildren { get; set; }
	public required DbSet<Category> Categories { get; set; }
	public required DbSet<Price> Prices { get; set; }
	public required DbSet<Discount> Discounts { get; set; }
	public required DbSet<FeaturedComponent> FeaturedComponents { get; set; }
	public required DbSet<Order> Orders { get; set; }
	public required DbSet<OrderComponent> OrderComponents { get; set; }

	public BackendContext(DbContextOptions<BackendContext> options) : base(options) { }

	/*protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<MenuProduct>()
			.HasOne(mp => mp.Menu)
			.WithMany(m => m.MenuProducts)
			.HasForeignKey("MenuId")
			.OnDelete(DeleteBehavior.Restrict);

		modelBuilder.Entity<MenuProduct>()
			.HasOne(mp => mp.Product)
			.WithMany(p => p.MenuProducts)
			.HasForeignKey("ProductId")
			.OnDelete(DeleteBehavior.Restrict);

		modelBuilder.Entity<Product>()
			.HasOne(p => p.Discount)
			.WithMany(d => d.Products)
			.HasForeignKey("DiscountId")
			.IsRequired(false)
			.OnDelete(DeleteBehavior.Restrict); 

		modelBuilder.Entity<Menu>()
			.HasOne(m => m.Discount)
			.WithMany(d => d.Menus)
			.HasForeignKey("DiscountId")
			.IsRequired(false)
			.OnDelete(DeleteBehavior.Restrict);

		modelBuilder.Entity<ComponentChild>()
			.HasOne(pi => pi.Product)
			.WithMany(p => p.ProductIngredients)
			.HasForeignKey("ProductId")
			.OnDelete(DeleteBehavior.Restrict); 

		
		modelBuilder.Entity<ComponentChild>()
			.HasOne(pi => pi.Ingredient)
			.WithMany(i => i.ProductIngredients)
			.HasForeignKey("IngredientId")
			.OnDelete(DeleteBehavior.Restrict);

		modelBuilder.Entity<ModifiedOrderComponent>()
			.HasOne(mopi => mopi.OrderProduct)
			.WithMany(op => op.ModifiedOrderProductIngredients)
			.HasForeignKey("OrderProductId")
			.OnDelete(DeleteBehavior.Restrict);

		
		modelBuilder.Entity<ModifiedOrderComponent>()
			.HasOne(mopi => mopi.Ingredient)
			.WithMany(i => i.ModifiedOrderProductIngredients)
			.HasForeignKey("OrderProductId")
			.OnDelete(DeleteBehavior.Restrict);

	}*/
}
