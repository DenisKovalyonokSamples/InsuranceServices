using DK.DashboardService.API.Queries.Results;
using DK.DashboardService.API.Queries.Types;
using MediatR;

namespace DK.DashboardService.API.Queries;

public class GetSalesTrendsQuery : IRequest<GetSalesTrendsResult>
{
    public string ProductCode { get; set; }
    public DateTime SalesDateFrom { get; set; }
    public DateTime SalesDateTo { get; set; }
    public TimeUnit Unit { get; set; }
}
