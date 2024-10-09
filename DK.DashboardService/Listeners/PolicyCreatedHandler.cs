using DK.DashboardService.Domain;
using DK.DashboardService.Domain.Contracts;
using DK.PolicyService.API.Events;
using MediatR;

namespace DK.DashboardService.Listeners;

public class PolicyCreatedHandler : INotificationHandler<PolicyCreated>
{
    private readonly IPolicyRepository policyRepository;

    public PolicyCreatedHandler(IPolicyRepository policyRepository)
    {
        this.policyRepository = policyRepository;
    }

    public Task Handle(PolicyCreated notification, CancellationToken cancellationToken)
    {
        var policy = new PolicyDocument
        (
            notification.PolicyNumber,
            notification.PolicyFrom,
            notification.PolicyTo,
            $"{notification.PolicyHolder.FirstName} {notification.PolicyHolder.LastName}",
            notification.ProductCode,
            notification.TotalPremium,
            notification.AgentLogin
        );

        policyRepository.Save(policy);

        return Task.CompletedTask;
    }
}
