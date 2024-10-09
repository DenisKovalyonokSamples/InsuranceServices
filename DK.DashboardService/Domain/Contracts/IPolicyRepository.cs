using DK.DashboardService.Domain.Results;

namespace DK.DashboardService.Domain.Contracts;

public interface IPolicyRepository
{
    void Save(PolicyDocument policy);

    PolicyDocument FindByNumber(string policyNumber);

    AgentSalesQueryResult GetAgentSales(AgentSalesQuery query);

    TotalSalesQueryResult GetTotalSales(TotalSalesQuery query);

    SalesTrendsResult GetSalesTrend(SalesTrendsQuery query);
}
