using ECommerce.Application.Features.Orders.Commands.CreateOrder.dtos;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Orders.Queries.GetOrderByUserId
{
    public class GetOrderByUserIdHandler : IRequestHandler<GetOrderByUserIdQuery, List<OrderResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetOrderByUserIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<OrderResponseDto>> Handle(GetOrderByUserIdQuery request, CancellationToken cancellationToken)
        {
            var orders = (await _unitOfWork.Repository<Order>()
                .GetAllIncludingAsync(o => o.OrderItems))
                .Where(o => o.UserId == request.UserId);

            var products = await _unitOfWork.Repository<Product>().GetAllAsync();

            return orders.Select(order => new OrderResponseDto
            {
                OrderId = order.Id,
                TotalAmount = order.TotalAmount,
                Discount = order.Discount,
                Items = order.OrderItems.Select(i =>
                {
                    var product = products.First(p => p.Id == i.ProductId);

                    return new OrderItemResponseDto
                    {
                        ProductId = i.ProductId,
                        ProductName = product.Name,
                        Quantity = i.Quantity,
                        Price = i.Price,
                        Total = i.Price * i.Quantity
                    };
                }).ToList()
            }).ToList();
        }
    }
}
