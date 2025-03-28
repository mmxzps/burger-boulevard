using Backend.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend;

public class BackendContext : DbContext
{
  public DbSet<Menu> Menus { get; set; }
  public DbSet<Product> Products { get; set; }
  public DbSet<Price> Prices { get; set; }
  public DbSet<Category> Categories { get; set; }
  public DbSet<Order> Orders { get; set; }
  public DbSet<OrderProduct> OrderProducts { get; set; }
  public DbSet<BaseProductIngredient> BaseProductIngredients { get; set; }
  public DbSet<PossibleProductIngredient> PossibleProductIngredients { get; set; }
  public DbSet<ModifiedOrderProductIngredient> ModifiedOrderProductIngredients { get; set; }
  public DbSet<Ingredient> Ingredients { get; set; }
  public DbSet<FeaturedProduct> FeaturedProducts { get; set; }

  public BackendContext(DbContextOptions<BackendContext> options) : base(options) {}
}
