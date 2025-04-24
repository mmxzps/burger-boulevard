using Backend.Models.Dto;
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

  // TODO Create proper safety guards around these endpoints.
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
	    .Include(o => o.Components)
			.ThenInclude(oc => oc.Component)
				.ThenInclude(c => c.ChildPolicies)
					.ThenInclude(cp => cp.Child)
	    .Include(o => o.Components)
			.ThenInclude(oc => oc.Parent)
	    .ToList() 
	    .Select(o => o.ToDto()));

  [HttpGet("{id}")]
    public async Task<ActionResult<Order>> Get(BackendContext context, int id) =>
      (await context.Orders
       .AsNoTracking()
       .Include(o => o.Components)
       .ThenInclude(oc => oc.Component)
       .FirstOrDefaultAsync(o => o.Id == id))?.ToDto() is Order order ?
      Ok(order) : NotFound();

	[HttpPut("{id}")]
	public async Task<ActionResult> UpdateStatus(BackendContext context, int id, [FromBody] OrderUpdateDto orderUpdateDto)
	{

		var order = await context.Orders
			.FindAsync(id);

		if (order == null)
			return NotFound();

		if (order.Status != orderUpdateDto.Status)
		{
			order.Status = orderUpdateDto.Status;
			await context.SaveChangesAsync();
			return Ok(order.ToDto());
		}
		
		return NoContent(); //If nothing changed, return 204 No Content
	}

  [HttpPost]
    public async Task<ActionResult<Order>> Create(BackendContext context, Models.Dto.Create.Order createOrder)
    {
        // TODO: Verify component policies

        var order = context.Orders
          .Add(new Models.Entities.Order
          {
              Status = Models.Entities.OrderStatus.Pending,
              TakeAway = createOrder.TakeAway,
              TotalPrice = 0
          });

        order.Entity.Components = createOrder.ToOrderComponentEntities(context, order.Entity);

        foreach (var oc in order.Entity.Components)
            oc.VerifyPolicies();

        order.Entity.TotalPrice = order.Entity.Components.Sum(oc => oc.EvaluatePrice());

        await context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(Create),
            (await context.Orders
             .AsNoTracking()
             .Include(o => o.Components)
             .ThenInclude(oc => oc.Component)
             .FirstOrDefaultAsync(o => o.Id == order.Entity.Id))?.ToDto());
    }
}
