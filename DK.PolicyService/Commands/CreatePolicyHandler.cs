﻿using DK.PolicyService.API.Commands.Dtos;
using DK.PolicyService.API.Commands.Results;
using DK.PolicyService.API.Commands;
using DK.PolicyService.API.Events;
using DK.PolicyService.Domain.Contracts;
using DK.PolicyService.Domain;
using MediatR;
using DK.PolicyService.Messaging.Contracts;

namespace DK.PolicyService.Commands;

public class CreatePolicyHandler : IRequestHandler<CreatePolicyCommand, CreatePolicyResult>
{
    private readonly IEventPublisher eventPublisher;
    private readonly IUnitOfWork uow;

    public CreatePolicyHandler(
        IUnitOfWork uow,
        IEventPublisher eventPublisher)
    {
        this.uow = uow;
        this.eventPublisher = eventPublisher;
    }

    public async Task<CreatePolicyResult> Handle(CreatePolicyCommand request, CancellationToken cancellationToken)
    {
        var offer = await uow.Offers.WithNumber(request.OfferNumber);
        var customer = new PolicyHolder
        (
            request.PolicyHolder.FirstName,
            request.PolicyHolder.LastName,
            request.PolicyHolder.TaxId,
            Address.Of
            (
                request.PolicyHolderAddress.Country,
                request.PolicyHolderAddress.ZipCode,
                request.PolicyHolderAddress.City,
                request.PolicyHolderAddress.Street
            )
        );
        var policy = offer.Buy(customer);

        uow.Policies.Add(policy);

        await eventPublisher.PublishMessage(PolicyCreated(policy));

        await uow.CommitChanges();

        return new CreatePolicyResult
        {
            PolicyNumber = policy.Number
        };
    }

    private static PolicyCreated PolicyCreated(Policy policy)
    {
        var version = policy.Versions.First(v => v.VersionNumber == 1);

        return new PolicyCreated
        {
            PolicyNumber = policy.Number,
            PolicyFrom = version.CoverPeriod.ValidFrom,
            PolicyTo = version.CoverPeriod.ValidTo,
            ProductCode = policy.ProductCode,
            TotalPremium = version.TotalPremiumAmount,
            PolicyHolder = new PersonDto
            {
                FirstName = version.PolicyHolder.FirstName,
                LastName = version.PolicyHolder.LastName,
                TaxId = version.PolicyHolder.Pesel
            },
            AgentLogin = policy.AgentLogin
        };
    }
}
