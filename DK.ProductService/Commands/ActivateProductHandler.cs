using DK.ProductService.API.Commands.Results;
using DK.ProductService.API.Commands;
using DK.ProductService.Domain.Contracts;
using MediatR;

namespace DK.ProductService.Commands;

public class ActivateProductHandler : IRequestHandler<ActivateProductCommand, ActivateProductResult>
{
    private readonly IProductRepository products;

    public ActivateProductHandler(IProductRepository products)
    {
        this.products = products;
    }

    public async Task<ActivateProductResult> Handle(ActivateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await products.FindById(request.ProductId);
        product.Activate();
        return new ActivateProductResult
        {
            ProductId = product.Id
        };
    }
}
