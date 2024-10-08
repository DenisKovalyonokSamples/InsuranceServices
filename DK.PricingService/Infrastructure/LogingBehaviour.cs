using MediatR;

namespace DK.PricingService.Infrastructure;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly ILogger<TRequest> logger;

    public LoggingBehavior(ILogger<TRequest> logger)
    {
        this.logger = logger;
    }

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling {@Command}", typeof(TRequest));
        return next();
    }
}

public static class LoggingBehaviorInstaller
{
    public static IServiceCollection AddLoggingBehavior(this IServiceCollection services)
    {
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        return services;
    }
}
