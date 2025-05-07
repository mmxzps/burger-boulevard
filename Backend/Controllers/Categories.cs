using Backend.Models.Dto;
using Backend.Models.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class Categories : ControllerBase
{
    private readonly ComponentService _componentService;

    public Categories(ComponentService componentService)
    {
	    _componentService = componentService;
    }
	[HttpGet]
  public ActionResult<IEnumerable<Category>> All(BackendContext context) =>
    Ok(context.Categories.AsNoTracking().Select(c => _componentService.ToCategoryDto(c)));
}
