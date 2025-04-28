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
        bool? standalone,
        Models.Entities.ComponentLevel? level,
        int? categoryId) =>
      Ok(context.Components
          .AsNoTracking()
          .Where(c =>
            (standalone == null || c.Standalone == standalone) &&
            (categoryId == null || c.Categories.Any(cat => cat.Id == categoryId)) &&
            (level == null || c.Level == level))
          .Select(c => c.ToDto()));

    [HttpGet("{id}")]
    public async Task<ActionResult<Component>> Get(BackendContext context, int id) =>
      await context.Components
        .AsNoTracking()
        .FirstOrDefaultAsync(c => c.Id == id) is Models.Entities.Component component ?
      Ok(component.ToDto()) : NotFound();

    [HttpGet("{id}/policies")]
    public async Task<ActionResult<Component>> Policies(BackendContext context, int id) =>
      await context.Components
        .AsNoTracking()
        .Include(c => c.ChildPolicies)
        .ThenInclude(p => p.Child)
        .FirstOrDefaultAsync(c => c.Id == id) is Models.Entities.Component component ?
      Ok(component.ChildPolicies.Select(p => p.ToDto())) : NotFound();

    [HttpGet("featured")]
    public ActionResult<IEnumerable<Component>> Featured(BackendContext context) =>
      Ok(context.FeaturedComponents.AsNoTracking().Select(c => c.ToDto()));
}
