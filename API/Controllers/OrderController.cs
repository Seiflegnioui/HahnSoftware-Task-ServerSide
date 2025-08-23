using hahn.Application.Buyer.Commands;
using hahn.Application.DTOs;
using hahn.Application.Order.Commands;
using hahn.Application.Order.Queries;
using hahn.Application.Seller.Commands;
using hahn.Domain.Entities;
using hahn.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hahn.API.Controllers
{
    [ApiController]
    [Route("order")]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(result.Errors);

            return Ok(result.Data);
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetOrder([FromQuery]int? sellerId,[FromQuery] int? buyerId)
        {
            var result = await _mediator.Send(new GetOrderQuery(){sellerId = sellerId, buyerId = buyerId});

            if (!result.Success)
                return BadRequest(result.Errors);

            return Ok(result.Datalist);
        }
    }

}