using Amazon.S3;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pathmaker.Application.Services.Files;
using Pathmaker.Infrastructure.Services.Files;

namespace Pathmaker.Infrastructure.Extensions;

// ReSharper disable once InconsistentNaming
public static class IServiceCollectionExtensions {
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) {
        services.AddSingleton<IAmazonS3, AmazonS3Client>();
        services.AddSingleton<IFileService, FileService>();
        return services;
    }
}
