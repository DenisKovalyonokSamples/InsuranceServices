using DK.PricingService.API.Commands;
using DK.PricingService.API.Commands.Dtos;
using DK.PricingService.API.Commands.Results;
using DK.PricingService.IntegrationTests.Fixtures;
using System.Net;
using Alba;
using Xunit;
using static Xunit.Assert;

namespace DK.PricingService.IntegrationTests;

[Collection("PricingControllerFixtureCollection")]
public class PricingControllerTest
{
    private readonly PricingControllerFixture fixture;

    public PricingControllerTest(PricingControllerFixture fixture)
    {
        this.fixture = fixture;
    }

    [Fact]
    public async Task PriceForTravelPolicyIsCorrect()
    {
        var response = await fixture.SystemUnderTest.Scenario(_ =>
        {
            _.Post
                .Json(new CalculatePriceCommand
                {
                    ProductCode = "TRI",
                    PolicyFrom = DateTimeOffset.Now.AddDays(5),
                    PolicyTo = DateTimeOffset.Now.AddDays(10),
                    SelectedCovers = new List<string> { "C1", "C2", "C3" },
                    Answers = new List<QuestionAnswer>
                    {
                        new NumericQuestionAnswer { QuestionCode = "NUM_OF_ADULTS", Answer = 1M },
                        new NumericQuestionAnswer { QuestionCode = "NUM_OF_CHILDREN", Answer = 1M },
                        new TextQuestionAnswer { QuestionCode = "DESTINATION", Answer = "EUR" }
                    }
                })
                .ToUrl("/api/Pricing");

            _.StatusCodeShouldBeOk();
        });

        var calculationResult = await response.ReadAsJsonAsync<CalculatePriceResult>();
        Equal(98M, calculationResult.TotalPrice);
    }

    [Fact]
    public async Task CommandIsProperlyValidated()
    {
        _ = await fixture.SystemUnderTest.Scenario(_ =>
        {
            _.Post
                .Json(new CalculatePriceCommand())
                .ToUrl("/api/Pricing");

            _.StatusCodeShouldBe(HttpStatusCode.BadRequest);
        });
    }
}
