using MediatR;

namespace DK.PaymentService.Infrastructure;

public class LogingBehaviour<TRequest, TRespone> : IPipelineBehavior<TRequest, TRespone>
{
    private readonly ILogger<TRequest> logger;

    public LogingBehaviour(ILogger<TRequest> logger)
    {
        this.logger = logger;
    }

    public Task<TRespone> Handle(TRequest request, RequestHandlerDelegate<TRespone> next,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling {@Command}", typeof(TRequest));
        return next();
    }
}

public static class LogingBehaviourInstaller
{
    public static IServiceCollection AddLogingBehaviour(this IServiceCollection services)
    {
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LogingBehaviour<,>));
        return services;
    }
}
