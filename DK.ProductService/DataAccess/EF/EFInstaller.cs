using DK.ProductService.Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DK.ProductService.DataAccess.EF;

public static class EFInstaller
{
    public static IServiceCollection AddEFConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var useInMemoryDatabase = configuration.GetSection("Settings").GetValue<bool>("UseInMemoryDatabase");

        services.AddDbContext<ProductDbContext>(options =>
        {
            if (useInMemoryDatabase)
                options.UseInMemoryDatabase("Products");
            else
                options.UseSqlServer(configuration.GetConnectionString("Products"));
        });

        services.AddScoped<IProductRepository, ProductRepository>();
        return services;
    }
}
