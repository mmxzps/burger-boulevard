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
  public required DbSet<Sale> Sales { get; set; }
  public required DbSet<Menu> Menus { get; set; }
  public required DbSet<FeaturedProduct> FeaturedProducts { get; set; }
  public required DbSet<Order> Orders { get; set; }
  public required DbSet<OrderProduct> OrderProducts { get; set; }
  public required DbSet<ModifiedOrderProductIngredient> ModifiedOrderProductIngredients { get; set; }

  public BackendContext(DbContextOptions<BackendContext> options) : base(options) {}
}
