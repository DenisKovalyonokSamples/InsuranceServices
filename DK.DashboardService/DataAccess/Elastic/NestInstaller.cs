﻿using DK.DashboardService.Domain;
using DK.DashboardService.Domain.Contracts;
using Elastic.Clients.Elasticsearch;

namespace DK.DashboardService.DataAccess.Elastic;

public static class NestInstaller
{
    public static IServiceCollection AddElasticSearch(this IServiceCollection services, string cnString)
    {
        services.AddSingleton(typeof(ElasticsearchClient), svc => CreateElasticClient(cnString));
        services.AddScoped(typeof(IPolicyRepository), typeof(ElasticPolicyRepository));
        return services;
    }

    private static ElasticsearchClient CreateElasticClient(string cnString)
    {
        var connectionSettings = new ElasticsearchClientSettings(new Uri(cnString))
            .DefaultMappingFor<PolicyDocument>(m =>
                m.IndexName("policy_lab_stats").IdProperty(d => d.Number));
        return new ElasticsearchClient(connectionSettings);
    }
}
