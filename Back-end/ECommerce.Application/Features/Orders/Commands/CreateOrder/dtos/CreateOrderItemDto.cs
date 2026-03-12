using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Orders.Commands.CreateOrder.dtos
{
    public class CreateOrderItemDto
    {
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
