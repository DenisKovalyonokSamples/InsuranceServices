using Polly;

namespace DK.PolicyService.Domain.Contracts;

public interface IPolicyRepository
{
    void Add(Policy policy);

    Task<Policy> WithNumber(string number);
}
