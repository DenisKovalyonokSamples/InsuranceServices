using DK.ProductService.API.Queries.Dtos;
using MediatR;

namespace DK.ProductService.API.Queries;

public class FindProductByCodeQuery : IRequest<ProductDto>
{
    public string ProductCode { get; set; }
}
