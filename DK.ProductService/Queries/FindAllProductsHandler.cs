﻿using DK.ProductService.API.Queries.Dtos;
using DK.ProductService.API.Queries;
using DK.ProductService.Domain.Contracts;
using DK.ProductService.Queries.Mappers;
using MediatR;

namespace DK.ProductService.Queries;

public class FindAllProductsHandler : IRequestHandler<FindAllProductsQuery, IEnumerable<ProductDto>>
{
    private readonly IProductRepository productRepository;

    public FindAllProductsHandler(IProductRepository productRepository)
    {
        this.productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<IEnumerable<ProductDto>> Handle(FindAllProductsQuery request, CancellationToken cancellationToken)
    {
        var result = await productRepository.FindAllActive();

        return result.Select(p => new ProductDto
        {
            Code = p.Code,
            Name = p.Name,
            Description = p.Description,
            Image = p.Image,
            MaxNumberOfInsured = p.MaxNumberOfInsured,
            Icon = p.ProductIcon,
            Questions = p.Questions != null ? ProductMapper.ToQuestionDtoList(p.Questions) : null,
            Covers = p.Covers.Any() ? ProductMapper.ToCoverDtoList(p.Covers) : null
        }).ToList();
    }
}
