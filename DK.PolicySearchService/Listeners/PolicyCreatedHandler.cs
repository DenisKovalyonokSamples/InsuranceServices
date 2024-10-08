using DK.PolicySearchService.Domain;
using DK.PolicySearchService.Domain.Contracts;
using DK.PolicyService.API.Events;
using MediatR;

namespace DK.PolicySearchService.Listeners;

public class PolicyCreatedHandler : INotificationHandler<PolicyCreated>
{
    private readonly IPolicyRepository policis;

    public PolicyCreatedHandler(IPolicyRepository policis)
    {
        this.policis = policis;
    }

    public async Task Handle(PolicyCreated notification, CancellationToken cancellationToken)
    {
        await policis.Add(new Policy
        {
            PolicyNumber = notification.PolicyNumber,
            PolicyStartDate = notification.PolicyFrom,
            PolicyEndDate = notification.PolicyTo,
            ProductCode = notification.ProductCode,
            PolicyHolder = $"{notification.PolicyHolder.FirstName} {notification.PolicyHolder.LastName}",
            PremiumAmount = notification.TotalPremium
        });
    }
}