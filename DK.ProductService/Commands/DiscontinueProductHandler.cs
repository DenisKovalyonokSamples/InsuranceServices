using DK.ProductService.API.Commands.Results;
using DK.ProductService.API.Commands;
using DK.ProductService.Domain.Contracts;
using MediatR;

namespace DK.ProductService.Commands;

public class DiscontinueProductHandler : IRequestHandler<DiscontinueProductCommand, DiscontinueProductResult>
{
    private readonly IProductRepository products;

    public DiscontinueProductHandler(IProductRepository products)
    {
        this.products = products;
    }

    public async Task<DiscontinueProductResult> Handle(DiscontinueProductCommand request,
        CancellationToken cancellationToken)
    {
        var product = await products.FindById(request.ProductId);
        product.Discontinue();
        return new DiscontinueProductResult
        {
            ProductId = product.Id
        };
    }
}
