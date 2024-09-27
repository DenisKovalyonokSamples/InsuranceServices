namespace DK.PolicyService.Domain.Contracts;

public interface IUnitOfWork : IDisposable
{
    IOfferRepository Offers { get; }

    IPolicyRepository Policies { get; }

    Task CommitChanges();
}
