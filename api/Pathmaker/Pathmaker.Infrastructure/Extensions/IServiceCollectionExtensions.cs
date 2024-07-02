using Amazon.Runtime;
using Amazon.S3;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pathmaker.Application.Services.Files;
using Pathmaker.Infrastructure.Services.Files;

namespace Pathmaker.Infrastructure.Extensions;

// ReSharper disable once InconsistentNaming
public static class IServiceCollectionExtensions {
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) {
        var fileOptions = new AwsOptions();
        configuration.GetSection(AwsOptions.Aws).Bind(fileOptions);
        var awsOptions = configuration.GetAWSOptions();
        awsOptions.Credentials = new BasicAWSCredentials(fileOptions.AccessKey, fileOptions.SecretKey);
        services.AddDefaultAWSOptions(awsOptions);
        services.AddAWSService<IAmazonS3>();
        services.AddSingleton<IFileService, FileService>();
        return services;
    }
}
