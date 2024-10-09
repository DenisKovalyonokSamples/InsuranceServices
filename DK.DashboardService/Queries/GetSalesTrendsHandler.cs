using DK.DashboardService.API.Queries.Dtos;
using DK.DashboardService.API.Queries.Results;
using DK.DashboardService.API.Queries;
using DK.DashboardService.Domain;
using MediatR;
using DK.DashboardService.Domain.Contracts;

namespace DK.DashboardService.Queries;

public class GetSalesTrendsHandler : IRequestHandler<GetSalesTrendsQuery, GetSalesTrendsResult>
{
    private readonly IPolicyRepository policyRepository;

    public GetSalesTrendsHandler(IPolicyRepository policyRepository)
    {
        this.policyRepository = policyRepository;
    }


    public Task<GetSalesTrendsResult> Handle(GetSalesTrendsQuery request, CancellationToken cancellationToken)
    {
        var queryResult = policyRepository.GetSalesTrend
        (
            new SalesTrendsQuery
            (
                request.ProductCode,
                request.SalesDateFrom,
                request.SalesDateTo,
                request.Unit.ToTimeAggregationUnit()
            )
        );

        return Task.FromResult(BuildResult(queryResult));
    }

    private GetSalesTrendsResult BuildResult(SalesTrendsResult queryResult)
    {
        var result = new GetSalesTrendsResult
        {
            PeriodsSales = new List<PeriodSaleDto>()
        };

        foreach (var periodSale in queryResult.PeriodSales)
            result.PeriodsSales.Add(new PeriodSaleDto
            {
                PeriodDate = periodSale.PeriodDate,
                Period = periodSale.Period,
                Sales = new SalesDto(periodSale.Sales.PoliciesCount, periodSale.Sales.PremiumAmount)
            });

        return result;
    }
}
