using MediatR;

namespace DK.PolicySearchService.API.Queries;

public class FindPolicyQuery : IRequest<FindPolicyResult>
{
    public string QueryText { get; set; }
}
