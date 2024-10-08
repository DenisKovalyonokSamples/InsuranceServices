using DK.PolicyService.Domain;

namespace DK.PolicyService.UnitTests.TestData;

public class PolicyFactory
{
    internal static Policy AlreadyTerminatedPolicy()
    {
        var offer = OfferFactory.NewOfferValidUntil(DateTime.Now.AddDays(5));

        var policy = offer.Buy(PolicyHolderFactory.Abc());

        policy.Terminate(DateTime.Now.AddDays(3));

        return policy;
    }
}