using Backend.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class Components : ControllerBase
{
  [HttpGet("levels")]
  public ActionResult<IDictionary<string, Models.Entities.ComponentLevel>> Levels() =>
    new Dictionary<string, Models.Entities.ComponentLevel>
    {
      { "ingredient", Models.Entities.ComponentLevel.Ingredient },
      { "product", Models.Entities.ComponentLevel.Product },
      { "menu", Models.Entities.ComponentLevel.Menu }
    };

  [HttpGet]
  public ActionResult<IEnumerable<Component>> Find(
      BackendContext context,
      Models.Entities.ComponentLevel? level,
      int? categoryId) =>
    Ok(context.Components
        .AsNoTracking()
        .Include(c => c.Categories)
        .Include(c => c.ChildPolicies)
        .Include(c => c.Price)
        .Where(c =>
          (categoryId == null || c.Categories.Any(cat => cat.Id == categoryId)) &&
          (level == null || c.Level == level))
        .Select(c => c.ToDto()));

  [HttpGet("{id}")]
  public async Task<ActionResult<Component>> Get(BackendContext context, int id) =>
    Ok((await context.Components
          .Include(c => c.Categories)
          .Include(c => c.ChildPolicies)
          .Include(c => c.Price)
          .FirstOrDefaultAsync(c => c.Id == id))?.ToDto());

  [HttpGet("featured")]
  public ActionResult<IEnumerable<Component>> Featured(BackendContext context) =>
    Ok(context.FeaturedComponents
        .AsNoTracking()
        .Include(c => c.Component)
        .ThenInclude(c => c.Categories)
        .Include(c => c.Component)
        .ThenInclude(c => c.ChildPolicies)
        .Include(c => c.Component)
        .ThenInclude(c => c.Price)
        .Select(c => c.ToDto()));
}
