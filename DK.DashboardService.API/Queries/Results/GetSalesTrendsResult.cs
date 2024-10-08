using DK.DashboardService.API.Queries.Dtos;

namespace DK.DashboardService.API.Queries.Results;

public class GetSalesTrendsResult
{
    public List<PeriodSaleDto> PeriodsSales { get; set; }
}
