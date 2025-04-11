using Backend.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Order = Backend.Models.Dto.Order;

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
	    .Include(o => o.Components)
	    .ThenInclude(oc => oc.Component)
	    .ThenInclude(c => c.Categories)
	    .Include(o => o.Components)
	    .ThenInclude(oc => oc.Component)
	    .ThenInclude(c => c.Price)
	    .ToList() 
	    .Select(o => o.ToDto()));

  [HttpGet("{id}")]
  public async Task<ActionResult<Order>> Get(BackendContext context, int id) =>
    Ok((await context.Orders
        .AsNoTracking()
        .FirstOrDefaultAsync(o => o.Id == id))?.ToDto());

	[HttpPut("{id}")]
	public async Task<ActionResult> UpdateStatus(BackendContext context, int id, [FromBody] OrderStatus status)
	{
		var order = await context.Orders
			.FindAsync(id);

		if (order == null)
			return NotFound();

		if (order.Status != status)
		{
			order.Status = status;
			await context.SaveChangesAsync();
			return Ok(order.ToDto());
		}
		
		return NoContent(); //If nothing changed, return 204 No Content
	}
	
}
