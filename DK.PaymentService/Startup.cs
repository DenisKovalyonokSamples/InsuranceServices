using DK.PaymentService.Domain;
using DK.PaymentService.Infrastructure;
using DK.PaymentService.Init;
using DK.PaymentService.Jobs;
using DK.PaymentService.Messaging.RabbitMq;
using DK.PolicyService.API.Events;
using DK.PaymentService.DataAccess.Marten;
using DK.PaymentService.Infrastructure.Configuration;
using GlobalExceptionHandler.WebApi;
using Newtonsoft.Json;

namespace DK.PaymentService;

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
        services.AddMvc()
            .AddNewtonsoftJson(opt => { opt.SerializerSettings.TypeNameHandling = TypeNameHandling.Auto; });

        services.AddMarten(Configuration.GetConnectionString("PgConnection"));
        services.AddPaymentDemoInitializer();
        services.AddMediatR(opts => opts.RegisterServicesFromAssemblyContaining<Startup>());
        services.AddLogingBehaviour();
        services.AddSingleton<PolicyAccountNumberGenerator>();
        services.AddRabbitListeners();
        services.AddBackgroundJobs(Configuration.GetSection("BackgroundJobs").Get<BackgroundJobsConfig>());
        services.AddSwaggerGen();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting();
        app.UseGlobalExceptionHandler(cfg => cfg.MapExceptions());
        if (!env.IsDevelopment()) app.UseHsts();

        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseInitializer();
        app.UseRabbitListeners(new List<Type> { typeof(PolicyCreated), typeof(PolicyTerminated) });
        app.UseBackgroundJobs();
        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}
