using Blazorise.Bootstrap5;
using Blazored.SessionStorage;
using Blazorise.Icons.FontAwesome;
using DK.BlazorWasmClient.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using DK.BlazorWasmClient.Components.PolicySearch;
using DK.BlazorWasmClient.Components.Products;
using DK.BlazorWasmClient.Components.Auth;
using DK.BlazorWasmClient.Components.Dashboard;
using DK.BlazorWasmClient;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");

        AddBlazorise(builder.Services);

        builder.Services.AddAuthorizationCore();

        builder.Services.AddBlazoredSessionStorage();
        builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
        builder.Services.AddHttpClient();

        builder.Services.AddScoped<PolicySearchClient>();
        builder.Services.AddScoped<ProductsClient>();
        builder.Services.AddScoped<PolicyClient>();
        builder.Services.AddScoped<AuthClient>();
        builder.Services.AddScoped<DashboardClient>();

        await builder.Build().RunAsync();
    }

    public static void AddBlazorise(IServiceCollection services)
    {
        services
            .AddBlazorise();
        services
            .AddBootstrap5Providers()
            .AddFontAwesomeIcons();
    }
}
