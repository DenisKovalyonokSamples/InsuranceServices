using DK.ProductService.API.Commands.Results;
using MediatR;

namespace DK.ProductService.API.Commands;

public class DiscontinueProductCommand : IRequest<DiscontinueProductResult>
{
    public Guid ProductId { get; set; }
}
