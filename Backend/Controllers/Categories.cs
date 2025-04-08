using Backend.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class Categories : ControllerBase
{
  [HttpGet]
  public ActionResult<IEnumerable<Category>> All(BackendContext context) =>
    Ok(context.Categories.AsNoTracking().Select(c => c.ToDto()));
}
