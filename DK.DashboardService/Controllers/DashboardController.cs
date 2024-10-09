using DK.DashboardService.API.Queries.Results;
using DK.DashboardService.API.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DK.DashboardService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly IMediator bus;

    public DashboardController(IMediator bus)
    {
        this.bus = bus;
    }

    [HttpPost("agents-sales")]
    public async Task<GetAgentsSalesResult> AgentsSales([FromBody] GetAgentsSalesQuery query)
    {
        return await bus.Send(query);
    }

    [HttpPost("total-sales")]
    public async Task<GetTotalSalesResult> TotalSales([FromBody] GetTotalSalesQuery query)
    {
        return await bus.Send(query);
    }

    [HttpPost("sales-trends")]
    public async Task<GetSalesTrendsResult> SalesTrends([FromBody] GetSalesTrendsQuery query)
    {
        return await bus.Send(query);
    }
};
