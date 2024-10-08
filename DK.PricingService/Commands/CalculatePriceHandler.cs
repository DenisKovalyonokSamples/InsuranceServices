using DK.PricingService.API.Commands.Results;
using DK.PricingService.API.Commands;
using DK.PricingService.Domain.Contracts;
using DK.PricingService.Domain;
using FluentValidation;
using MediatR;

namespace DK.PricingService.Commands;

public class CalculatePriceHandler : IRequestHandler<CalculatePriceCommand, CalculatePriceResult>
{
    private readonly CalculatePriceCommandValidator commandValidator = new();
    private readonly IDataStore dataStore;

    public CalculatePriceHandler(IDataStore dataStore)
    {
        this.dataStore = dataStore;
    }

    public async Task<CalculatePriceResult> Handle(CalculatePriceCommand cmd, CancellationToken cancellationToken)
    {
        await commandValidator.ValidateAndThrowAsync(cmd);

        var tariff = await dataStore.Tariffs[cmd.ProductCode];

        var calculation = tariff.CalculatePrice(ToCalculation(cmd));

        return ToResult(calculation);
    }

    private static Calculation ToCalculation(CalculatePriceCommand cmd)
    {
        return new Calculation(
            cmd.ProductCode,
            cmd.PolicyFrom,
            cmd.PolicyTo,
            cmd.SelectedCovers,
            cmd.Answers.ToDictionary(a => a.QuestionCode, a => a.GetAnswer()));
    }

    private static CalculatePriceResult ToResult(Calculation calculation)
    {
        return new CalculatePriceResult
        {
            TotalPrice = calculation.TotalPremium,
            CoverPrices = calculation.Covers.ToDictionary(c => c.Key, c => c.Value.Price)
        };
    }
}
