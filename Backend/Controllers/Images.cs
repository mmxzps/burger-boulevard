using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class Images : ControllerBase
{
  [HttpGet("{id}")]
  [Produces("image/png", "application/json")]
  public async Task<IActionResult> Get(BackendContext context, int id) =>
    await context.Images.FindAsync(id) is Models.Entities.Image image
    ? File(image.Data, "image/png") : NotFound();
}
