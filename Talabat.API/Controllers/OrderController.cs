using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.Core.Dtos.OrderDtos;
using Talabat.Core.Entities.OrderModule;
using Talabat.Core.Exceptions;
using Talabat.Core.Services.Contract;
using Talabat.Core.Shared.ErrorModels;

namespace Talabat.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly IOrderService _orderService;
		private readonly IMapper _mapper;

		public OrderController(IOrderService orderService, IMapper mapper)
		{
			_orderService = orderService;
			_mapper = mapper;
		}

		[ProducesResponseType(typeof(OrderToReturnDto), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ErrorToReturn), StatusCodes.Status400BadRequest)]
		[HttpPost]
		[Authorize]
		public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
		{
			var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
			var Address = _mapper.Map<Address>(orderDto.ShippingAddress);
			var Order = await _orderService.CreateOrderAsync(BuyerEmail, orderDto.BasketId, orderDto.DeliveryMethodId, Address);
			if (Order is null)
				throw new BadRequestException("There is a problem with your order");

			var MappedOrder = _mapper.Map<OrderToReturnDto>(Order);
			return Ok(MappedOrder);
		}

		[ProducesResponseType(typeof(IReadOnlyList<OrderToReturnDto>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ErrorToReturn), StatusCodes.Status404NotFound)]
		[HttpGet]
		[Authorize]
		public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForUser()
		{
			var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
			var Orders = await _orderService.GetOrdersForSpecificUserAsync(BuyerEmail);
			if (Orders is null)
				throw new OrderNotFoundException("No Orders Found For This User");
			var MappedOrders = _mapper.Map<IReadOnlyList<OrderToReturnDto>>(Orders);
			return Ok(MappedOrders);
		}

	}
}
