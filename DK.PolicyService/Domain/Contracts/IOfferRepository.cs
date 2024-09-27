namespace DK.PolicyService.Domain.Contracts;

public interface IOfferRepository
{
    void Add(Offer offer);

    Task<Offer> WithNumber(string number);
}
