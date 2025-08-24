
using hahn.Application.order.Commands;
using hahn.Application.order.Queries;
using hahn.Domain.Entities;
using hahn.Domain.Enums;
using hahn.notifications.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hahn.API.Controllers
{
    [ApiController]
    [Route("notifications")]
    [Authorize]
    public class NotificationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NotificationController(IMediator mediator)
        {
            _mediator = mediator;
        }


       

        [HttpGet("get")]
        public async Task<IActionResult> GetNotifications([FromQuery] int notifiedId)
        {
            var result = await _mediator.Send(new GetNotificationsQuery() { notifiedId = notifiedId });

            if (!result.Success)
                return BadRequest(result.Errors);

            return Ok(result.Datalist);
        }
    }

}