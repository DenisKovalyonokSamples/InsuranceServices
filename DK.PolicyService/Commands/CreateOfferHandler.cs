﻿using DK.PolicyService.API.Commands.Results;
using DK.PolicyService.API.Commands;
using DK.PolicyService.Domain.Contracts;
using DK.PolicyService.Domain;
using MediatR;

namespace DK.PolicyService.Commands;

public class CreateOfferHandler : IRequestHandler<CreateOfferCommand, CreateOfferResult>
{
    private readonly IPricingService pricingService;
    private readonly IUnitOfWork uow;

    public CreateOfferHandler(IUnitOfWork uow, IPricingService pricingService)
    {
        this.uow = uow;
        this.pricingService = pricingService;
    }

    public async Task<CreateOfferResult> Handle(CreateOfferCommand request, CancellationToken cancellationToken)
    {
        //calculate price
        var priceParams = ConstructPriceParams(request);
        var price = await pricingService.CalculatePrice(priceParams);


        var o = Offer.ForPrice(
            priceParams.ProductCode,
            priceParams.PolicyFrom,
            priceParams.PolicyTo,
            null,
            price
        );

        //create and save offer
        uow.Offers.Add(o);
        await uow.CommitChanges();

        //return result
        return ConstructResult(o);
    }

    private CreateOfferResult ConstructResult(Offer o)
    {
        return new CreateOfferResult
        {
            OfferNumber = o.Number,
            TotalPrice = o.TotalPrice,
            CoversPrices = o.Covers.ToDictionary(c => c.Code, c => c.Price)
        };
    }

    private PricingParams ConstructPriceParams(CreateOfferCommand request)
    {
        return new PricingParams
        {
            ProductCode = request.ProductCode,
            PolicyFrom = request.PolicyFrom,
            PolicyTo = request.PolicyTo,
            SelectedCovers = request.SelectedCovers,
            Answers = request.Answers.Select(a => Answer.Create(a.QuestionType, a.QuestionCode, a.GetAnswer())).ToList()
        };
    }
}
