namespace DK.PricingService.API.Commands.Results;

public class CalculatePriceResult
{
    public decimal TotalPrice { get; set; }
    public Dictionary<string, decimal> CoverPrices { get; set; }

    public static CalculatePriceResult Empty()
    {
        return new CalculatePriceResult { TotalPrice = 0M, CoverPrices = new Dictionary<string, decimal>() };
    }
}
