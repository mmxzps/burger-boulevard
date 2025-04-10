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

  // TODO Create proper safety guards around these endpoints.
  [HttpGet]
  public ActionResult<IEnumerable<Order>> All(BackendContext context) =>
    Ok(context.Orders
      .AsNoTracking()
      .Include(o => o.Components)
      .ThenInclude(oc => oc.Component)
      .Select(o => o.ToDto()));

  [HttpGet("{id}")]
  public async Task<ActionResult<Order>> Get(BackendContext context, int id) =>
    (await context.Orders
     .AsNoTracking()
     .Include(o => o.Components)
     .ThenInclude(oc => oc.Component)
     .FirstOrDefaultAsync(o => o.Id == id))?.ToDto() is Order order ?
    Ok(order) : NotFound();

  [HttpPost]
  public async Task<ActionResult<Order>> Create(BackendContext context, Models.Dto.Create.Order createOrder)
  {
    var order = context.Orders
      .Add(new Models.Entities.Order
        {
        Status = Models.Entities.OrderStatus.Pending,
        TakeAway = createOrder.TakeAway,
        });
    order.Entity.Components = createOrder.ToOrderComponentEntities(context, order.Entity);
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
