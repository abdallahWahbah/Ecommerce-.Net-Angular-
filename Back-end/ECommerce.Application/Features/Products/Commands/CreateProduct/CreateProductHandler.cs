using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                StockQuantity = request.StockQuantity,
                CategoryId = request.CategoryId
            };

            await _unitOfWork.Repository<Product>().AddAsync(product);

            await _unitOfWork.SaveChangesAsync();

            return product.Id;
        }
    }
}
