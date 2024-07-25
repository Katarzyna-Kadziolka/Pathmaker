using Amazon.Runtime;
using Amazon.S3;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pathmaker.Application.Services.Files;
using Pathmaker.Infrastructure.Services.Files;

namespace Pathmaker.Infrastructure.Extensions;

// ReSharper disable once InconsistentNaming
public static class IServiceCollectionExtensions {
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) {
        services.AddAws(configuration);
        services.AddSingleton<IFileService, FileService>();
        services.AddHangfire(configuration);
        return services;
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder builder) {
        builder.UseRouting();
        builder.UseHangfireDashboard();
        builder.UseEndpoints(endpoints => { endpoints.MapHangfireDashboard(); });
        RecurringJob.AddOrUpdate<FileCleanupJob>("cleanup", job => job.Cleanup(), Cron.Minutely);
        return builder;
    }

    private static void AddAws(this IServiceCollection services, IConfiguration configuration) {
        var options = configuration.GetSection(AwsOptions.SectionName).Get<AwsOptions>();
        ArgumentNullException.ThrowIfNull(options);
        var awsOptions = configuration.GetAWSOptions();
        awsOptions.Credentials = new BasicAWSCredentials(options.AccessKey, options.SecretKey);
        awsOptions.DefaultClientConfig.ServiceURL = options.ServiceUrl;
        services.AddDefaultAWSOptions(awsOptions);
        services.AddAWSService<IAmazonS3>();
    }

    private static void AddHangfire(this IServiceCollection services, IConfiguration configuration) {
        services.AddHangfire(config =>
            config
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UsePostgreSqlStorage(c =>
                c.UseNpgsqlConnection(configuration.GetConnectionString("Default"))));
        services.AddHangfireServer();
    }
}
