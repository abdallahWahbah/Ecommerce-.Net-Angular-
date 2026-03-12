using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateOrderValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x.UserId)
                .NotEmpty()
                .MustAsync(UserExists)
                .WithMessage("User does not exist.");

            RuleFor(x => x.Items)
                .NotEmpty()
                .WithMessage("Order must contain at least one item.");

            RuleFor(x => x.Items) // prevent duplicate items in the same order
                .Must(items => items.Select(i => i.ProductId).Distinct().Count() == items.Count)
                .WithMessage("Duplicate products are not allowed in the same order.");

            RuleForEach(x => x.Items).ChildRules(item =>
            {
                item.RuleFor(i => i.ProductId)
                    .NotEmpty()
                    .MustAsync(ProductExists)
                    .WithMessage("Product does not exist.");

                item.RuleFor(i => i.Quantity)
                    .GreaterThan(0)
                    .WithMessage("Quantity must be greater than 0.");
            });
        }

        private async Task<bool> UserExists(Guid userId, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork
                .Repository<User>()
                .GetByIdAsync(userId);

            return user != null;
        }

        private async Task<bool> ProductExists(Guid productId, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork
                .Repository<Product>()
                .GetByIdAsync(productId);

            return product != null;
        }
    }
}
