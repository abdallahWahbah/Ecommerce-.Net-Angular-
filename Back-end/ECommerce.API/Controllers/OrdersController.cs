using ECommerce.Application.Features.Orders.Commands.CreateOrder;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}
