using ECommerce.Application.Common;
using ECommerce.Application.Features.Products.Dtos;
using ECommerce.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Products.Queries.GetProducts
{
    public class GetProductsQuery : IRequest<PagedResult<ProductDto>>
    {
        public string? Search { get; set; }

        public Guid? CategoryId { get; set; }

        public string? SortBy { get; set; }

        public string? SortDirection { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}
