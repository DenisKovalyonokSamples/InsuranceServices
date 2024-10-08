using DK.ProductService.API.Commands.Results;
using MediatR;

namespace DK.ProductService.API.Commands;

public class ActivateProductCommand : IRequest<ActivateProductResult>
{
    public Guid ProductId { get; set; }
}
