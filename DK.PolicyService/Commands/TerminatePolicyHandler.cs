﻿using DK.PolicyService.API.Commands.Dtos;
using DK.PolicyService.API.Commands.Results;
using DK.PolicyService.API.Commands;
using DK.PolicyService.API.Events;
using DK.PolicyService.Domain.Contracts;
using DK.PolicyService.Domain;
using MediatR;
using DK.PolicyService.Messaging.Contracts;

namespace DK.PolicyService.Commands;

public class TerminatePolicyHandler : IRequestHandler<TerminatePolicyCommand, TerminatePolicyResult>
{
    private readonly IEventPublisher eventPublisher;
    private readonly IUnitOfWork uow;

    public TerminatePolicyHandler(IUnitOfWork uow, IEventPublisher eventPublisher)
    {
        this.uow = uow;
        this.eventPublisher = eventPublisher;
    }

    public async Task<TerminatePolicyResult> Handle(TerminatePolicyCommand request, CancellationToken cancellationToken)
    {
        var policy = await uow.Policies.WithNumber(request.PolicyNumber);

        var terminationResult = policy.Terminate(request.TerminationDate);

        await eventPublisher.PublishMessage(PolicyTerminated(terminationResult));

        await uow.CommitChanges();

        return new TerminatePolicyResult
        {
            PolicyNumber = policy.Number,
            MoneyToReturn = terminationResult.AmountToReturn
        };
    }

    private PolicyTerminated PolicyTerminated(PolicyTerminationResult terminationResult)
    {
        return new PolicyTerminated
        {
            PolicyNumber = terminationResult.TerminalVersion.Policy.Number,
            PolicyFrom = terminationResult.TerminalVersion.CoverPeriod.ValidFrom,
            PolicyTo = terminationResult.TerminalVersion.CoverPeriod.ValidTo,
            ProductCode = terminationResult.TerminalVersion.Policy.ProductCode,
            TotalPremium = terminationResult.TerminalVersion.TotalPremiumAmount,
            AmountToReturn = terminationResult.AmountToReturn,
            PolicyHolder = new PersonDto
            {
                FirstName = terminationResult.TerminalVersion.PolicyHolder.FirstName,
                LastName = terminationResult.TerminalVersion.PolicyHolder.LastName,
                TaxId = terminationResult.TerminalVersion.PolicyHolder.Pesel
            }
        };
    }
}
