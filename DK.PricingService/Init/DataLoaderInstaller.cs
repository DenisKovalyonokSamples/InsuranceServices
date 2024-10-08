﻿namespace DK.PricingService.Init;

public static class DataLoaderInstaller
{
    public static IServiceCollection AddPricingDemoInitializer(this IServiceCollection services)
    {
        services.AddScoped<DataLoader>();
        return services;
    }
}
