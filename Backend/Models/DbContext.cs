using Backend.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend;

public class BackendContext : DbContext
{
	public required DbSet<Ingredient> Ingredients { get; set; }
	public required DbSet<Product> Products { get; set; }
	public required DbSet<ProductIngredient> ProductIngredients { get; set; }
	public required DbSet<Category> Categories { get; set; }
	public required DbSet<Price> Prices { get; set; }
	public required DbSet<Discount> Discounts { get; set; }
	public required DbSet<Menu> Menus { get; set; }
	public required DbSet<FeaturedProduct> FeaturedProducts { get; set; }
	public required DbSet<Order> Orders { get; set; }
	public required DbSet<OrderProduct> OrderProducts { get; set; }
	public required DbSet<ModifiedOrderProductIngredient> ModifiedOrderProductIngredients { get; set; }

	public BackendContext(DbContextOptions<BackendContext> options) : base(options) { }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

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

		modelBuilder.Entity<ProductIngredient>()
			.HasOne(pi => pi.Product)
			.WithMany(p => p.ProductIngredients)
			.HasForeignKey("ProductId")
			.OnDelete(DeleteBehavior.Restrict); 

		
		modelBuilder.Entity<ProductIngredient>()
			.HasOne(pi => pi.Ingredient)
			.WithMany(i => i.ProductIngredients)
			.HasForeignKey("IngredientId")
			.OnDelete(DeleteBehavior.Restrict);

		modelBuilder.Entity<ModifiedOrderProductIngredient>()
			.HasOne(mopi => mopi.OrderProduct)
			.WithMany(op => op.ModifiedOrderProductIngredients)
			.HasForeignKey("OrderProductId")
			.OnDelete(DeleteBehavior.Restrict);

		
		modelBuilder.Entity<ModifiedOrderProductIngredient>()
			.HasOne(mopi => mopi.Ingredient)
			.WithMany(i => i.ModifiedOrderProductIngredients)
			.HasForeignKey("OrderProductId")
			.OnDelete(DeleteBehavior.Restrict);

	}
}
