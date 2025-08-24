using hahn.Application.DTOs;
using hahn.Application.seller.Commands;
using hahn.Application.seller.Queries;
using hahn.Domain.Entities;
using hahn.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hahn.API.Controllers
{
    [ApiController]
    [Route("seller")]
    [Authorize(Roles = nameof(RolesEnum.SELLER))]
    public class SellerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SellerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]

        public async Task<IActionResult> CreateSeller([FromForm] CreateSellerCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(result.Errors);

            return Ok(result.Data);
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetSeller( [FromQuery] int id)
        {
            var result = await _mediator.Send(new GetSellerByIdQuery(id));

            if (!result.Success)
                return BadRequest(result.Errors);

            return Ok(result.Data);
        }

        
    }

}