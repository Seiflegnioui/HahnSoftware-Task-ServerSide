
using hahn.Application.order.Commands;
using hahn.Application.order.Queries;
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


        [Authorize(Roles = nameof(RolesEnum.BUYER))]
        [HttpPost("create")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(result.Errors);

            return Ok(result.Data);
        }


        [HttpGet("get")]
        public async Task<IActionResult> GetOrder([FromQuery] int? sellerId, [FromQuery] int? buyerId)
        {
            var result = await _mediator.Send(new GetOrderQuery() { sellerId = sellerId, buyerId = buyerId });

            if (!result.Success)
                return BadRequest(result.Errors);

            return Ok(result.Datalist);
        }

        [Authorize(Roles = nameof(RolesEnum.SELLER))]
        [HttpPut("state")]
        public async Task<IActionResult> UpdateOrderState([FromBody] UpdateOrderStateCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(result.Errors);

            return Ok(result.Data);
        }
    }

}