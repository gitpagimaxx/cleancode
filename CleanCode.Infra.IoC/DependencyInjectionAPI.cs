using CleanCode.Application.Interfaces;
using CleanCode.Application.Mappings;
using CleanCode.Application.Services;
using CleanCode.Domain.Account;
using CleanCode.Domain.Interfaces;
using CleanCode.Infra.Data.Context;
using CleanCode.Infra.Data.Indentity;
using CleanCode.Infra.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanCode.Infra.IoC;

public static class DependencyInjectionAPI
{
    public static IServiceCollection AddInfrastructureAPI(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options => 
            options.UseSqlServer(configuration.GetConnectionString("DBConnection"),
            b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        // identity
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        // repositories
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        // services
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IProductService, ProductService>();

        // account
        services.AddScoped<IAuthenticate, AuthenticateService>();

        // AutoMapper
        services.AddAutoMapper(typeof(DomainToDTOMappingProfiler));

        // MediatR
        var handlers = AppDomain.CurrentDomain.Load("CleanCode.Application");
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(handlers));

        

        return services;
    }
}
