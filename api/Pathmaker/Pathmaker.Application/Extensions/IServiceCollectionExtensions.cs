using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pathmaker.Application.Behaviour;
using Pathmaker.Persistence;

namespace Pathmaker.Application.Extensions;

// ReSharper disable once InconsistentNaming
public static class IServiceCollectionExtensions {
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration, IHostEnvironment hostEnvironment) {
        services.AddMediatR(typeof(IApplicationMarker));
        services.AddMapper();
        services.AddFluentValidation();
        services.AddDbContext(configuration, hostEnvironment);
        return services;
    }

    public static IApplicationBuilder UseApplication(
        this IApplicationBuilder builder) {
        return builder.UseMiddleware<ApplicationExceptionMiddleware>();
    }

    private static void AddDbContext(this IServiceCollection services, IConfiguration configuration, IHostEnvironment hostEnvironment) {
        string connectionString;
        if (hostEnvironment.IsProduction()) {
            connectionString = new HerokuDbConnector.HerokuDbConnector().Build();
        }
        else {
            connectionString = configuration.GetConnectionString("Default");
        }
        
        services.AddDbContext<ApplicationDbContext>(o => o.UseNpgsql(connectionString),
                optionsLifetime: ServiceLifetime.Singleton)
            .AddDbContextFactory<ApplicationDbContext>();
    }

    private static void AddFluentValidation(this IServiceCollection services) {
        services.AddValidatorsFromAssemblyContaining<IApplicationMarker>(includeInternalTypes: true);
        services.AddFluentValidationAutoValidation();
    }

    private static void AddMapper(this IServiceCollection services) {
        var config = new TypeAdapterConfig();
        config.Scan(Assembly.GetAssembly(typeof(IApplicationMarker))!);
        services.AddSingleton(config);
        services.AddSingleton<IMapper, ServiceMapper>();
    }
}
