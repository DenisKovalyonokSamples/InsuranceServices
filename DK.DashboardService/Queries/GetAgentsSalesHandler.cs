using DK.DashboardService.API.Queries.Dtos;
using DK.DashboardService.API.Queries.Results;
using DK.DashboardService.API.Queries;
using DK.DashboardService.Domain;
using MediatR;
using DK.DashboardService.Domain.Contracts;

namespace DK.DashboardService.Queries;

public class GetAgentsSalesHandler : IRequestHandler<GetAgentsSalesQuery, GetAgentsSalesResult>
{
    private readonly IPolicyRepository policyRepository;

    public GetAgentsSalesHandler(IPolicyRepository policyRepository)
    {
        this.policyRepository = policyRepository;
    }

    public Task<GetAgentsSalesResult> Handle(GetAgentsSalesQuery request, CancellationToken cancellationToken)
    {
        var queryResult = policyRepository.GetAgentSales
        (
            new AgentSalesQuery
            (
                request.AgentLogin,
                request.ProductCode,
                request.SalesDateFrom,
                request.SalesDateTo
            )
        );

        return Task.FromResult(BuildResult(queryResult));
    }

    private GetAgentsSalesResult BuildResult(AgentSalesQueryResult queryResult)
    {
        var result = new GetAgentsSalesResult
        {
            PerAgentTotal = new Dictionary<string, SalesDto>()
        };

        foreach (var agentResult in queryResult.PerAgentTotal)
            result.PerAgentTotal[agentResult.Key] = new SalesDto
            {
                PoliciesCount = agentResult.Value.PoliciesCount,
                PremiumAmount = agentResult.Value.PremiumAmount
            };

        return result;
    }
}
