using Xunit;

namespace DK.PricingService.IntegrationTests.Fixtures;

[CollectionDefinition("PricingControllerFixtureCollection")]
public class PricingControllerFixtureCollection : ICollectionFixture<PricingControllerFixture>
{
}
