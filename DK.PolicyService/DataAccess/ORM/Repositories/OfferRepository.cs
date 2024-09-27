using DK.PolicyService.Domain.Contracts;
using DK.PolicyService.Domain;
using NHibernate.Linq;

namespace DK.PolicyService.DataAccess.ORM.Repositories;

public class OfferRepository : IOfferRepository
{
    private readonly NHibernate.ISession session;

    public OfferRepository(NHibernate.ISession session)
    {
        this.session = session;
    }

    public void Add(Offer offer)
    {
        session.Save(offer);
    }

    public async Task<Offer> WithNumber(string number)
    {
        return await session.Query<Offer>()
            .FirstOrDefaultAsync(o => o.Number == number);
    }
}
