using DK.DashboardService.API.Queries.Dtos;

namespace DK.DashboardService.API.Queries.Results;

public class GetAgentsSalesResult
{
    public IDictionary<string, SalesDto> PerAgentTotal { get; set; }
}
