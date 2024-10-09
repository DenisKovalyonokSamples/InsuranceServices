namespace DK.DashboardService.Init;

public class SalesDataInitializer : IHostedService
{
    private readonly IServiceProvider serviceProvider;

    public SalesDataInitializer(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();
        await scope.ServiceProvider.GetService<SalesData>().SeedData();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
