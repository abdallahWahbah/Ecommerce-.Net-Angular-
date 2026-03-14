using ECommerce.Application.Common;
using ECommerce.Application.Features.Products.Dtos;
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
    public class GetProductsHandler : IRequestHandler<GetProductsQuery, PagedResult<ProductDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProductsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResult<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.Repository<Product>().GetAllIncludingAsync(p => p.Category);

            var query = products.AsQueryable();

            // Search
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                query = query.Where(p =>
                    p.Name.Contains(request.Search, StringComparison.OrdinalIgnoreCase) ||
                    p.Description.Contains(request.Search, StringComparison.OrdinalIgnoreCase));
            }

            // Filter
            if (request.CategoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == request.CategoryId);
            }

            // Sorting
            if (!string.IsNullOrEmpty(request.SortBy))
            {
                if (request.SortBy.ToLower() == "price")
                {
                    query = request.SortDirection == "desc"
                        ? query.OrderByDescending(p => p.Price)
                        : query.OrderBy(p => p.Price);
                }

                if (request.SortBy.ToLower() == "name")
                {
                    query = request.SortDirection == "desc"
                        ? query.OrderByDescending(p => p.Name)
                        : query.OrderBy(p => p.Name);
                }
            }

            var totalCount = query.Count();

            var pagedProducts = query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            var result = pagedProducts.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                StockQuantity = p.StockQuantity,
                ImageUrl = p.ImageUrl,
                CategoryName = p.Category?.Name
            }).ToList();

            return new PagedResult<ProductDto>
            {
                Items = result,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}