using Backend.Models.Entities;

namespace Backend.Models.Dto;

public class OrderUpdateDto
{
	public required OrderStatus Status { get; set; }
}

