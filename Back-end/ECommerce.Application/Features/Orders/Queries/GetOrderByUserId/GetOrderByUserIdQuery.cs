using ECommerce.Application.Features.Orders.Commands.CreateOrder.dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Orders.Queries.GetOrderByUserId
{
    public class GetOrderByUserIdQuery : IRequest<List<OrderResponseDto>>
    {
        public Guid UserId { get; set; }
    }
}
