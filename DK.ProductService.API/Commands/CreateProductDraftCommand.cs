using DK.ProductService.API.Commands.Dtos;
using DK.ProductService.API.Commands.Results;
using MediatR;

namespace DK.ProductService.API.Commands;

public class CreateProductDraftCommand : IRequest<CreateProductDraftResult>
{
    public ProductDraftDto ProductDraft { get; set; }
}
