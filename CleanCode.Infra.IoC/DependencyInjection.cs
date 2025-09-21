using CleanCode.Application.Interfaces;
using CleanCode.Application.Mappings;
using CleanCode.Application.Services;
using CleanCode.Domain.Interfaces;
using CleanCode.Infra.Data.Context;
using CleanCode.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanCode.Infra.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options => 
            options.UseSqlServer(configuration.GetConnectionString("DBConnection"),
            b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        // repositories
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        // services
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IProductService, ProductService>();

        // AutoMapper
        services.AddAutoMapper(typeof(DomainToDTOMappingProfiler));

        // MediatR
        var handlers = AppDomain.CurrentDomain.Load("CleanCode.Application");
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(handlers));

        return services;
    }
}
