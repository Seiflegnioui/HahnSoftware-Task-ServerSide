using hahn.Application.DTOs;
using hahn.Application.Users.Commands;
using hahn.Application.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace hahn.API.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromForm] CreateUserCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success)
            {
                return BadRequest(result.Errors);
            }


            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserCommand command)
        {
            var result = await _mediator.Send(command);

            if (result == null)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result);
        }

        [HttpGet("connected")]
        public async Task<IActionResult> GetConnectedUser()
        {
            var result = await _mediator.Send(new GetConnectedUserQuery());

            if (!result.Success)
            {
                return BadRequest(result.Errors);
            }


            return Ok(result);
        }

    }

}