using ECommerce.Application.Features.Notifications.Commands.MarkNotificationAsRead;
using ECommerce.Application.Features.Notifications.Queries.GetNotifications;
using ECommerce.Application.Features.Notifications.Queries.UnreadNotificationsCount;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotificationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NotificationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetMyNotifications()
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userId = Guid.Parse(userIdString);
            var result = await _mediator.Send(new GetNotificationsQuery(userId));

            return Ok(result);
        }

        [HttpPut("{id}/read")]
        public async Task<IActionResult> MarkAsRead(Guid id)
        {
            var result = await _mediator.Send(new MarkNotificationAsReadCommand(id));

            if (!result)
                return NotFound();

            return Ok();
        }

        [HttpGet("unread-count")]
        public async Task<IActionResult> GetUnreadCount()
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userId = Guid.Parse(userIdString);
            var result = await _mediator.Send(new GetUnreadNotificationsCountQuery(userId));

            return Ok(result);
        }
    }
}
