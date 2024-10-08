using DK.ProductService.API.Queries.Dtos;
using DK.ProductService.API.Queries;
using DK.ProductService.Domain.Contracts;
using DK.ProductService.Queries.Mappers;
using MediatR;

namespace DK.ProductService.Queries;

public class FindProductByCodeHandler : IRequestHandler<FindProductByCodeQuery, ProductDto>
{
    private readonly IProductRepository productRepository;

    public FindProductByCodeHandler(IProductRepository productRepository)
    {
        this.productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<ProductDto> Handle(FindProductByCodeQuery request, CancellationToken cancellationToken)
    {
        var result = await productRepository.FindOne(request.ProductCode);

        return result != null
            ? new ProductDto
            {
                Code = result.Code,
                Name = result.Name,
                Description = result.Description,
                Image = result.Image,
                MaxNumberOfInsured = result.MaxNumberOfInsured,
                Icon = result.ProductIcon,
                Questions = result.Questions != null ? ProductMapper.ToQuestionDtoList(result.Questions) : null,
                Covers = result.Covers != null ? ProductMapper.ToCoverDtoList(result.Covers) : null
            }
            : null;
    }
}
