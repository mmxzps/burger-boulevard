using Backend.Controllers;
using Backend.Migrations;
using Backend.Models.Dto;
using Backend.Models.Entities;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Order = Backend.Models.Dto.Order;
using Humanizer;
using NuGet.Protocol.Plugins;

namespace Backend.Models.Services
{
	public class OrderService
	{
		private readonly ComponentService _componentService;
		private readonly IngredientComparerService _comparerService;

		public OrderService(IngredientComparerService comparerService, ComponentService componentService)
		{
			_comparerService = comparerService;
			_componentService = componentService;
		}
		public Dto.Order ToOrderDto(Entities.Order order) => new Dto.Order
		{
			Id = order.Id,
			Status = order.Status,
			Components = order.Components.Select(ToOrderComponentDto),
			TakeAway = order.TakeAway,
			TotalPrice = order.TotalPrice,
			OrderTime = order.OrderTime
		};

		public Dto.OrderQueue ToQueueDto(Entities.Order order) => new Dto.OrderQueue
		{
			Id = order.Id,
			OrderTime = order.OrderTime
		};

		public Dto.OrderComponent ToOrderComponentDto(Entities.OrderComponent orderComponent) => new Dto.OrderComponent
		{
			Id = orderComponent.Id,
			Component = _componentService.ToComponentDto(orderComponent.Component),
			Parent = orderComponent.ParentId,
			AddedComponents = _comparerService.GetAddedIngredients(orderComponent),
			RemovedComponents = _comparerService.GetRemovedIngredients(orderComponent),
			TotalPrice = orderComponent.EvaluatePrice()
		};
		
		
		
	}
}
