using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Products.Queries.GetProducts
{
    public class GetProductsHandler : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProductsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Repository<Product>().GetAllAsync();
        }
    }
}
