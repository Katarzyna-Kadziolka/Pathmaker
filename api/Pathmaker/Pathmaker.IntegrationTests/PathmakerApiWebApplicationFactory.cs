using System.Data.Common;
using Amazon.S3;
using Amazon.S3.Model;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;
using Respawn;
using Pathmaker.Api;
using Pathmaker.Application.Services.Files;
using Pathmaker.Persistence;
using Pathmaker.Shared.Features;
using Pathmaker.Shared.Services.DateTimeProviders;
using Pathmaker.Tests.Shared.Services.DateTimeProviders;
using Testcontainers.PostgreSql;

namespace Pathmaker.IntegrationTests;

public class PathmakerApiWebApplicationFactory : WebApplicationFactory<IApiMarker> {
    private readonly PostgreSqlContainer _postgreSqlContainer = new PostgreSqlBuilder()
        .WithDatabase("application")
        .WithUsername("postgres")
        .WithPassword("password")
        .Build();

    private IContainer _awsContainer = LocalAwsContainer.GetContainer();

    public HttpClient HttpClient { get; private set; } = default!;
    private DbConnection _dbConnection = default!;
    private Respawner _respawner = default!;

    public async Task ResetDatabase() {
        await _respawner.ResetAsync(_dbConnection);
    }

    public async Task InitializeAsync() {
        await _postgreSqlContainer.StartAsync();
        await _awsContainer.StartAsync();
        HttpClient = CreateClient();
        await InitializeRespawner();
        

    }

    // ReSharper disable once IdentifierTypo
    private async Task InitializeRespawner() {
        _dbConnection = new NpgsqlConnection(_postgreSqlContainer.GetConnectionString());
        await _dbConnection.OpenAsync();
        _respawner = await Respawner.CreateAsync(_dbConnection, new RespawnerOptions {
            DbAdapter = DbAdapter.Postgres,
            WithReseed = true,
            SchemasToInclude = new[] {
                "public"
            }
        });
    }

    public override async ValueTask DisposeAsync() {
        await base.DisposeAsync();
        await _dbConnection.CloseAsync();
        await _postgreSqlContainer.StopAsync();
        await _awsContainer.StopAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder) {
        base.ConfigureWebHost(builder);

        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?> {
                ["ConnectionStrings:Default"] = _postgreSqlContainer.GetConnectionString(),
                [$"{AwsOptions.SectionName}:{nameof(AwsOptions.ServiceUrl)}"] =
                    $"http://{_awsContainer.Hostname}:{_awsContainer.GetMappedPublicPort(LocalAwsContainer.LocalStackPort)}",
                [$"{FeatureFlags.SectionName}:{nameof(FeatureFlags.Sentry)}"] = "false"
            }).Build();
        builder.UseConfiguration(config);
        


        // Doesn't work in .Net 6: https://github.com/dotnet/aspnetcore/issues/37680
        // builder.ConfigureAppConfiguration((configBuilder) =>
        // {
        //     configBuilder.AddInMemoryCollection();
        // });
        base.ConfigureWebHost(builder);
        builder.ConfigureAppConfiguration(configBuilder => {
            configBuilder.AddInMemoryCollection(
                new Dictionary<string, string?> {
                    ["Serilog:MinimumLevel:Override:Microsoft"] = "Warning"
                });
        });

        builder.ConfigureServices(services => {
            var sp = services.BuildServiceProvider();

            using var scope = sp.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<ApplicationDbContext>();
            db.Database.Migrate();

            CreateAwsS3Bucket(scopedServices);
        });

        builder.ConfigureTestServices(services => {
            services.RemoveAll<IDateTimeProvider>();
            services.AddSingleton<IDateTimeProvider, TestDateTimeProvider>();
            

        });
    }

    private static void CreateAwsS3Bucket(IServiceProvider scopedServices) {
        var client = scopedServices.GetRequiredService<IAmazonS3>();
        var request = new PutBucketRequest {
            BucketName = "pathmaker",
            UseClientRegion = true
        };
        client.PutBucketAsync(request).GetAwaiter().GetResult();
    }
}
