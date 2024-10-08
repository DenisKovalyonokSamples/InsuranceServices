namespace DK.PolicySearchService.Domain.Contracts;

public interface IPolicyRepository
{
    Task Add(Policy policy);

    Task<List<Policy>> Find(string queryText);
}
