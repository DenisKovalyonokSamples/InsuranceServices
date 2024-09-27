using DK.PolicyService.API.Commands.Dtos;
using DK.PolicyService.API.Commands.Results;
using MediatR;

namespace DK.PolicyService.API.Commands;

public class CreatePolicyCommand : IRequest<CreatePolicyResult>
{
    public string OfferNumber { get; set; }
    public PersonDto PolicyHolder { get; set; }
    public AddressDto PolicyHolderAddress { get; set; }
}
