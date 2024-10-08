namespace DK.PricingService.Domain.Contracts;

public interface ITariffRepository
{
    Task<Tariff> this[string code] { get; }
    Task<Tariff> WithCode(string code);

    void Add(Tariff tariff);

    Task<bool> Exists(string code);
}
