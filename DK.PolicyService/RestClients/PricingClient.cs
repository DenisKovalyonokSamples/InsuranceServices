using DK.PolicyService.RestClients.Contracts;
using DK.PricingService.API.Commands.Results;
using DK.PricingService.API.Commands;
using Polly.Retry;
using RestEase;
using Steeltoe.Common.Discovery;
using Steeltoe.Discovery;
using Polly;

namespace DK.PolicyService.RestClients;

public class PricingClient : IPricingClient
{
    private static readonly AsyncRetryPolicy retryPolicy = Policy
        .Handle<HttpRequestException>()
        .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(3));

    private readonly IPricingClient client;

    public PricingClient(IConfiguration configuration, IDiscoveryClient discoveryClient)
    {
        var handler = new DiscoveryHttpClientHandler(discoveryClient);
        var httpClient = new HttpClient(handler, false)
        {
            BaseAddress = new Uri(configuration.GetValue<string>("PricingServiceUri"))
        };
        client = RestClient.For<IPricingClient>(httpClient);
    }

    public Task<CalculatePriceResult> CalculatePrice([Body] CalculatePriceCommand cmd)
    {
        return retryPolicy.ExecuteAsync(async () => await client.CalculatePrice(cmd));
    }
}