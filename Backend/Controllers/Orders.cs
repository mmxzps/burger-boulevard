using Backend.Models.Dto;
using Backend.Models.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Order = Backend.Models.Dto.Order;

namespace Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class Orders : ControllerBase
{
	private readonly OrderService _orderService;

	public Orders(OrderService orderService)
	{
		_orderService = orderService;
	}
    // TODO: Create proper safety guards around these endpoints.
    [HttpGet]
    public ActionResult<IEnumerable<Order>> All(BackendContext context) =>
      Ok(context.Orders
        .AsNoTracking()
        .Include(o => o.Components)
			.ThenInclude(oc => oc.Component)
				.ThenInclude(c => c.ChildPolicies)
					.ThenInclude(cp => cp.Child)
        .Select(_orderService.ToOrderDto));

    [HttpGet("Queue")]
    public ActionResult<IEnumerable<Order>> AllQueue(BackendContext context) =>
	    Ok(context.Orders.AsNoTracking()
		    .Select(o => _orderService.ToQueueDto(o)).ToList());

    [HttpGet("{id}")]
	public async Task<ActionResult<Order>> Get(BackendContext context, int id)
	{
		var entity = await context.Orders
			.AsNoTracking()
			.Include(o => o.Components)
			.ThenInclude(oc => oc.Component)
			.FirstOrDefaultAsync(o => o.Id == id);

		return entity is null
			? NotFound()
			: Ok(_orderService.ToOrderDto(entity));
	}

	[HttpPut("{id}")]
    public async Task<ActionResult> UpdateStatus(BackendContext context, int id, [FromBody] OrderUpdateDto orderUpdateDto)
    {
        var order = await context.Orders.FindAsync(id);

        if (order == null)
            return NotFound();

        if (order.Status != orderUpdateDto.Status)
        {
            order.Status = orderUpdateDto.Status;
            await context.SaveChangesAsync();
            return Ok(_orderService.ToOrderDto(order));
        }

        return NoContent(); // If nothing changed, return 204 No Content
    }

    [HttpPost]
	public async Task<ActionResult<Order>> Create(BackendContext context, Models.Dto.Create.Order createOrder)
	{
		var order = context.Orders.Add(new Models.Entities.Order
		{
			Status = Models.Entities.OrderStatus.Pending,
			TakeAway = createOrder.TakeAway,
			TotalPrice = 0
		});

		order.Entity.Components = createOrder.ToOrderComponentEntities(context, order.Entity);

		foreach (var oc in order.Entity.Components)
		{
			if (!oc.Component.Independent)
				throw new Exception($"Non-independent component '{oc.Component.Name}' ({oc.Component.Id}) cannot be defined as top-level.");
			oc.VerifyPolicies();
		}

		order.Entity.TotalPrice = order.Entity.Components.Sum(oc => oc.EvaluatePrice());

		await context.SaveChangesAsync();

		var createdOrder = await context.Orders
			.AsNoTracking()
			.Include(o => o.Components)
			.ThenInclude(oc => oc.Component)
			.ThenInclude(c => c.ChildPolicies)
			.ThenInclude(cp => cp.Child)
			.FirstOrDefaultAsync(o => o.Id == order.Entity.Id);

		if (createdOrder is null)
			return Problem("Order was created but could not be retrieved.");

		return CreatedAtAction(nameof(Create), _orderService.ToOrderDto(createdOrder));
	}
}
