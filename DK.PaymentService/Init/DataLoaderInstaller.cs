﻿namespace DK.PaymentService.Init;

public static class DataLoaderInstaller
{
    public static IServiceCollection AddPaymentDemoInitializer(this IServiceCollection services)
    {
        services.AddScoped<DataLoader>();
        return services;
    }
}
