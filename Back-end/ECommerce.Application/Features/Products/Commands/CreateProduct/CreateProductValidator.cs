using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using FluentValidation;

namespace ECommerce.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(200);

            RuleFor(x => x.Price)
                .GreaterThan(0);

            RuleFor(x => x.StockQuantity)
                .GreaterThanOrEqualTo(10);

            RuleFor(x => x.CategoryId)
                .NotEmpty()
                .MustAsync(CategoryExists)
                .WithMessage("Category does not exist.");
        }

        private async Task<bool> CategoryExists(Guid categoryId, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork
                .Repository<Category>()
                .GetByIdAsync(categoryId);

            return category != null;
        }
    }
}