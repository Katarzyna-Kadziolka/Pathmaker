using Amazon;
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
        var options = configuration.GetSection(AwsOptions.SectionName).Get<AwsOptions>();
        ArgumentNullException.ThrowIfNull(options);
        var awsOptions = configuration.GetAWSOptions();
        awsOptions.Credentials = new BasicAWSCredentials(options.AccessKey, options.SecretKey);
        awsOptions.Region = RegionEndpoint.EUNorth1;
        awsOptions.DefaultClientConfig.ServiceURL = options.ServiceUrl;
        services.AddDefaultAWSOptions(awsOptions);
        services.AddAWSService<IAmazonS3>();
        services.AddSingleton<IFileService, FileService>();
        return services;
    }
}
