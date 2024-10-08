﻿using DK.PolicyService.Domain.Contracts;
using DK.PolicyService.Domain;
using DK.PolicyService.RestClients.Contracts;
using DK.PricingService.API.Commands;
using DK.PricingService.API.Commands.Dtos;

namespace DK.PolicyService.RestClients;

public class PricingService : IPricingService
{
    private readonly IPricingClient pricingClient;

public PricingService(IPricingClient pricingClient)
{
    this.pricingClient = pricingClient;
}

public async Task<Price> CalculatePrice(PricingParams pricingParams)
{
    var cmd = new CalculatePriceCommand
    {
        ProductCode = pricingParams.ProductCode,
        PolicyFrom = pricingParams.PolicyFrom,
        PolicyTo = pricingParams.PolicyTo,
        SelectedCovers = pricingParams.SelectedCovers,
        Answers = ConstructAnswers(pricingParams.Answers)
    };

    var result = await pricingClient.CalculatePrice(cmd);

    return new Price(result.CoverPrices);
}

private List<QuestionAnswer> ConstructAnswers(List<Answer> answers)
{
    return answers.Select(a => ToQuestionAnswer(a)).ToList();
}

private QuestionAnswer ToQuestionAnswer(Answer a)
{
    if (a is TextAnswer)
        return new TextQuestionAnswer
        {
            QuestionCode = a.QuestionCode,
            Answer = (string)a.GetAnswerValue()
        };

    if (a is ChoiceAnswer)
        return new ChoiceQuestionAnswer
        {
            QuestionCode = a.QuestionCode,
            Answer = (string)a.GetAnswerValue()
        };

    if (a is NumericAnswer)
        return new NumericQuestionAnswer
        {
            QuestionCode = a.QuestionCode,
            Answer = (decimal)a.GetAnswerValue()
        };

    throw new ArgumentException("Unexpectd answer type " + a.GetType().Name);
}
}
