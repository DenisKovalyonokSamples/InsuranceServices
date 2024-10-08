﻿using DK.PolicyService.Messaging.Contracts;
using DK.PolicyService.Messaging.RabbitMq.Outbox;
using EasyNetQ;
using EasyNetQ.Topology;

namespace DK.PolicyService.Messaging.RabbitMq;

public static class RabbitInstaller
{
    public static IServiceCollection AddRabbitListeners(this IServiceCollection services)
    {
        var host = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true" ? "rabbit" : "localhost";
        var connectionStr = $"host={host}:5672;username=guest;password=guest";
        var bus = RabbitHutch.CreateBus(connectionStr);
        bus.Advanced.ExchangeDeclare("lab-dotnet-micro", ExchangeType.Topic);
        services.AddSingleton(bus);

        services.AddScoped<IEventPublisher, OutboxEventPublisher>();
        services.AddSingleton<Outbox.Outbox>();
        services.AddHostedService<OutboxSendingService>();
        return services;
    }
}
