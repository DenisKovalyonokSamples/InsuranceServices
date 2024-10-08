using DK.PaymentService.Domain.Contracts;
using DK.PolicyService.API.Events;
using MediatR;

namespace DK.PaymentService.Listeners;

public class PolicyTerminatedHandler : INotificationHandler<PolicyTerminated>
{
    private readonly IDataStore dataStore;

    public PolicyTerminatedHandler(IDataStore dataStore)
    {
        this.dataStore = dataStore;
    }

    public async Task Handle(PolicyTerminated notification, CancellationToken cancellationToken)
    {
        var policyAccount = await dataStore.PolicyAccounts.FindByNumber(notification.PolicyNumber);

        policyAccount.Close(notification.PolicyTo, notification.AmountToReturn);

        dataStore.PolicyAccounts.Update(policyAccount);

        await dataStore.CommitChanges();
    }
}
