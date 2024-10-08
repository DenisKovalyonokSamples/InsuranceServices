using DK.DashboardService.API.Queries.Results;
using MediatR;

namespace DK.DashboardService.API.Queries;

public class GetTotalSalesQuery : IRequest<GetTotalSalesResult>
{
    public string ProductCode { get; set; }
    public DateTime SalesDateFrom { get; set; }
    public DateTime SalesDateTo { get; set; }
}
