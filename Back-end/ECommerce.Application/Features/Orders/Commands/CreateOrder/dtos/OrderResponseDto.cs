using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Orders.Commands.CreateOrder.dtos
{
    public class OrderResponseDto
    {
        public Guid OrderId { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal Discount { get; set; }

        public List<OrderItemResponseDto> Items { get; set; }
    }
}
