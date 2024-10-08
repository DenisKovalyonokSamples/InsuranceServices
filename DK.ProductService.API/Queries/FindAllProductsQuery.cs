using DK.ProductService.API.Queries.Dtos;
using MediatR;

namespace DK.ProductService.API.Queries;

public class FindAllProductsQuery : IRequest<IEnumerable<ProductDto>>
{
}
