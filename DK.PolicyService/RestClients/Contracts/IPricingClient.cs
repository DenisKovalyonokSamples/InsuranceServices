using DK.PricingService.API.Commands.Results;
using DK.PricingService.API.Commands;
using RestEase;

namespace DK.PolicyService.RestClients.Contracts;

public interface IPricingClient
{
    [Post]
    Task<CalculatePriceResult> CalculatePrice([Body] CalculatePriceCommand cmd);
}
