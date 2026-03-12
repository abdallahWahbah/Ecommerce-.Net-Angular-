using ECommerce.Application.Features.Notifications.Commands.CreateNotification;
using ECommerce.Application.Features.Orders.Commands.CreateOrder.dtos;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, OrderResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public CreateOrderHandler(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<OrderResponseDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            decimal totalAmount = 0;

            var order = new Order
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                CreatedAt = DateTime.UtcNow,
                Discount = 0,
                TotalAmount = 0,
                OrderItems = new List<OrderItem>()
            };

            foreach (var item in request.Items)
            {
                var product = await _unitOfWork
                    .Repository<Product>()
                    .GetByIdAsync(item.ProductId);

                if (product == null)
                    throw new ValidationException("Product not found");

                if (product.StockQuantity < item.Quantity)
                    //throw new ValidationException($"Not enough stock for product {product.Name}");
                    throw new ValidationException(new[]
                    {
                        new ValidationFailure("StockQuantity", $"Not enough stock for product {product.Name}")
                    });

                var itemTotal = product.Price * item.Quantity;

                totalAmount += itemTotal;

                product.StockQuantity -= item.Quantity;

                var orderItem = new OrderItem
                {
                    Id = Guid.NewGuid(),
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    Price = product.Price
                };

                order.OrderItems.Add(orderItem);
            }

            // Discount
            if (totalAmount > 1000)
            {
                order.Discount = totalAmount * 0.1m;
            }

            order.TotalAmount = totalAmount - order.Discount;

            await _unitOfWork.Repository<Order>().AddAsync(order);

            await _unitOfWork.SaveChangesAsync();

            // Notification
            await _mediator.Send(new CreateNotificationCommand
            {
                UserId = order.UserId,
                Title = "Order Created",
                Message = "Your order has been placed successfully",
                Type = "Success"
            });

            return new OrderResponseDto
            {
                OrderId = order.Id,
                TotalAmount = order.TotalAmount,
                Discount = order.Discount,
                Items = order.OrderItems.Select(i => new OrderItemResponseDto
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    Price = i.Price,
                    Total = i.Price * i.Quantity
                }).ToList()
            };
        }
    }
}
