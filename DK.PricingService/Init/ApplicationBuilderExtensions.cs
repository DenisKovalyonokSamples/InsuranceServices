namespace DK.PricingService.Init;

public static class ApplicationBuilderExtensions
{
    public static async Task UseInitializer(this IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var initializer = scope.ServiceProvider.GetService<DataLoader>();
            await initializer.Seed();
        }
    }
}
