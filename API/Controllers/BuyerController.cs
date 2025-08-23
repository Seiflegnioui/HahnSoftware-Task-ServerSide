using hahn.Application.Buyer.Commands;
using hahn.Application.Buyer.Queries;
using hahn.Application.DTOs;
using hahn.Application.Seller.Commands;
using hahn.Domain.Entities;
using hahn.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hahn.API.Controllers
{
    [ApiController]
    [Route("buyer")]
    [Authorize(Roles = nameof(RolesEnum.BUYER))]
    public class BuyerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BuyerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateBuyer(CreateBuyerCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(result.Errors);

            return Ok(result.Data);
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetBuyer([FromQuery] int id)
        {
            var result = await _mediator.Send(new GetBuyerByIdQuery(id));

            if (!result.Success)
                return BadRequest(result.Errors);

            return Ok(result.Data);
        }
    }

}