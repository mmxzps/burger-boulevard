using Backend.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class Orders : ControllerBase
{
  [HttpGet("statuses")]
  public ActionResult<IDictionary<string, Models.Entities.OrderStatus>> Statuses() =>
    Ok(new Dictionary<string, Models.Entities.OrderStatus>() {
        { "pending", Models.Entities.OrderStatus.Pending },
        { "preparing", Models.Entities.OrderStatus.Preparing },
        { "done", Models.Entities.OrderStatus.Done },
        });

  // TODO Fix proper safety guards around these endpoints.
  [HttpGet]
  public ActionResult<IEnumerable<Order>> All(BackendContext context) =>
    Ok(context.Orders
      .AsNoTracking()
      .Select(o => o.ToDto()));

  [HttpGet("{id}")]
  public async Task<ActionResult<Order>> Get(BackendContext context, int id) =>
    Ok((await context.Orders
        .AsNoTracking()
        .FirstOrDefaultAsync(o => o.Id == id))?.ToDto());
}
