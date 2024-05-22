using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pathmaker.Application.Services.Emails;
using Pathmaker.Infrastructure.Services.Emails;

namespace Pathmaker.Infrastructure.Extensions;

// ReSharper disable once InconsistentNaming
public static class IServiceCollectionExtensions {
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) {
        services.AddScoped<IEmailService, EmailService>();
        return services;
    }
}