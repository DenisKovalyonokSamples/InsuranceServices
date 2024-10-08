using DK.PaymentService.API.Queries.Results;
using MediatR;

namespace DK.PaymentService.API.Queries;

public class GetAccountBalanceQuery : IRequest<GetAccountBalanceQueryResult>
{
    public string PolicyNumber { get; set; }
}
