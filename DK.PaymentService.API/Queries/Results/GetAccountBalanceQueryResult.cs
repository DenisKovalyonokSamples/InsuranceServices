using DK.PaymentService.API.Queries.Dtos;

namespace DK.PaymentService.API.Queries.Results;

public class GetAccountBalanceQueryResult
{
    public PolicyAccountBalanceDto Balance { get; set; }
}
