namespace DK.PricingService.Domain.Contracts;

public interface IDataStore : IDisposable
{
    ITariffRepository Tariffs { get; }

    Task CommitChanges();
}

