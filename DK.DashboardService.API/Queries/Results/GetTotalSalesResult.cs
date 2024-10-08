using DK.DashboardService.API.Queries.Dtos;

namespace DK.DashboardService.API.Queries.Results;

public class GetTotalSalesResult
{
    public SalesDto Total { get; set; }

    public Dictionary<string, SalesDto> PerProductTotal { get; set; }
}
