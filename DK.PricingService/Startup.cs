using DK.PricingService.Infrastructure;
using DK.PricingService.Init;
using DK.PricingService.Infrastructure.Configuration;
using DK.PricingService.DataAccess;
using GlobalExceptionHandler.WebApi;
using Newtonsoft.Json;
using Steeltoe.Discovery.Client;

namespace DK.PricingService;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDiscoveryClient(Configuration);
        services.AddControllers()
            .AddNewtonsoftJson(opt => { opt.SerializerSettings.TypeNameHandling = TypeNameHandling.Auto; });

        services.AddMarten(Configuration.GetConnectionString("DefaultConnection"));
        services.AddPricingDemoInitializer();
        services.AddMediatR(options => options.RegisterServicesFromAssemblyContaining<Program>());
        services.AddLoggingBehavior();
        services.AddSwaggerGen();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting();
        app.UseGlobalExceptionHandler(cfg => cfg.MapExceptions());

        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseInitializer();
        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}
