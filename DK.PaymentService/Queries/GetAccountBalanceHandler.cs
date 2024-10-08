using DK.PaymentService.API.Exceptions;
using DK.PaymentService.API.Queries.Dtos;
using DK.PaymentService.API.Queries.Results;
using DK.PaymentService.API.Queries;
using MediatR;
using DK.PaymentService.Domain.Contracts;

namespace DK.PaymentService.Queries;

public class GetAccountBalanceHandler : IRequestHandler<GetAccountBalanceQuery, GetAccountBalanceQueryResult>
{
    private readonly IDataStore dataStore;

    public GetAccountBalanceHandler(IDataStore dataStore)
    {
        this.dataStore = dataStore;
    }

    public async Task<GetAccountBalanceQueryResult> Handle(GetAccountBalanceQuery request,
        CancellationToken cancellationToken)
    {
        var policyAccount = await dataStore.PolicyAccounts.FindByNumber(request.PolicyNumber);

        if (policyAccount == null) throw new PolicyAccountNotFound(request.PolicyNumber);

        return new GetAccountBalanceQueryResult
        {
            Balance = new PolicyAccountBalanceDto
            {
                PolicyNumber = policyAccount.PolicyNumber,
                PolicyAccountNumber = policyAccount.PolicyAccountNumber,
                Balance = policyAccount.BalanceAt(DateTimeOffset.Now)
            }
        };
    }
}
