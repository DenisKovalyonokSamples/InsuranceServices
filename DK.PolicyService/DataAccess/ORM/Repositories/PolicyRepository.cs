using DK.PolicyService.Domain;
using DK.PolicyService.Domain.Contracts;
using NHibernate;
using NHibernate.Linq;

namespace DK.PolicyService.DataAccess.ORM.Repositories;

public class PolicyRepository : IPolicyRepository
{
    private readonly NHibernate.ISession session;

    public PolicyRepository(NHibernate.ISession session)
    {
        this.session = session;
    }

    public void Add(Policy policy)
    {
        session.Save(policy);
    }

    public async Task<Policy> WithNumber(string number)
    {
        return await session.Query<Policy>().FirstOrDefaultAsync(p => p.Number == number);
    }
}
