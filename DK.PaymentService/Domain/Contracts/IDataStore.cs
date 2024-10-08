namespace DK.PaymentService.Domain.Contracts;

public interface IDataStore : IDisposable
{
    IPolicyAccountRepository PolicyAccounts { get; }

    Task CommitChanges();
}
