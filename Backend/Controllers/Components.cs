using Backend.Models.Dto;
using Backend.Models.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class Components : ControllerBase
{
	private readonly ComponentService _componentService;
	public Components(ComponentService componentService)
	{
		_componentService = componentService;
	}
	[HttpGet]
    public ActionResult<IEnumerable<Component>> Find(
        BackendContext context,
        bool? independent,
        Models.Entities.ComponentLevel? level,
        int? categoryId) =>
      Ok(context.Components
          .Include(c => c.ChildPolicies)
          .Where(c =>
            (independent == null || c.Independent == independent) &&
            (categoryId == null || c.Categories.Any(cat => cat.Id == categoryId)) &&
            (level == null || c.Level == level))
          .Select(c => _componentService.ToComponentDto(c)));

    [HttpGet("{id}")]
    public async Task<ActionResult<Component>> Get(BackendContext context, int id) =>
      // FIX: For some reason, deep child policies not loaded, unlike the other - quantitative - functions.
      await context.Components
        .Include(c => c.ChildPolicies)
        .FirstOrDefaultAsync(c => c.Id == id) is Models.Entities.Component component ?
      Ok(_componentService.ToComponentDto(component)) : NotFound();

    [HttpGet("featured")]
    public ActionResult<IEnumerable<Component>> Featured(BackendContext context) =>
      Ok(context.FeaturedComponents
          .AsNoTracking()
          .Include(fc => fc.Component)
          .ThenInclude(c => c.ChildPolicies)
          .Select(c => _componentService.ToFeaturedComponentDto(c)));
}
