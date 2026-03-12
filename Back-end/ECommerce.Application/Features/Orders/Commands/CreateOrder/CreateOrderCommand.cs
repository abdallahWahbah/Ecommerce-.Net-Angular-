using ECommerce.Application.Features.Orders.Commands.CreateOrder.dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<OrderResponseDto>
    {
        public Guid UserId { get; set; }

        public List<CreateOrderItemDto> Items { get; set; }
    }
}
