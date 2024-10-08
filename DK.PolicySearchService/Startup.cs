using DK.PolicySearchService.DataAccess.ElasticSearch;
using DK.PolicySearchService.Messaging.RabbitMq;
using DK.PolicyService.API.Events;
using Steeltoe.Discovery.Client;

namespace DK.PolicySearchService;

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
        services.AddRabbitListeners();
        services.AddSwaggerGen();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting();
        if (env.IsDevelopment())
            app.UseDeveloperExceptionPage();
        else
            app.UseHsts();

        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseRabbitListeners(new List<Type> { typeof(PolicyCreated) });
        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}
