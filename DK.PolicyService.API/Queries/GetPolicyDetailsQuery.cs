using MediatR;

namespace DK.PolicyService.API.Queries;

public class GetPolicyDetailsQuery : IRequest<GetPolicyDetailsQueryResult>
{
    public string PolicyNumber { get; set; }
}
