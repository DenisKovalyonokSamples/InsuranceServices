using DK.PolicySearchService.API.Queries.Dtos;

namespace DK.PolicySearchService.API.Queries;

public class FindPolicyResult
{
    public List<PolicyDto> Policies { get; set; }
}
