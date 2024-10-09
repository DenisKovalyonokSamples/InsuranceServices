using DK.DashboardService.DataAccess.Elastic;
using DK.DashboardService.Domain.Contracts;
using DK.DashboardService.Init;
using DK.DashboardService.Messaging.RabbitMq;
using DK.PolicyService.API.Events;
using Steeltoe.Discovery.Client;

namespace DK.DashboardService;

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
        services.AddMvc()
            .AddNewtonsoftJson();
        services.AddMediatR(opts => opts.RegisterServicesFromAssemblyContaining<Startup>());
        services.AddElasticSearch(Configuration.GetConnectionString("ElasticSearchConnection"));
        services.AddSingleton<IPolicyRepository, ElasticPolicyRepository>();
        services.AddRabbitListeners(Configuration.GetSection("RabbitMqOptions").Get<RabbitMqOptions>());
        services.AddInitialSalesData();
        services.AddSwaggerGen();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

        app.UseRabbitListeners(new List<Type> { typeof(PolicyCreated) });
    }
}
