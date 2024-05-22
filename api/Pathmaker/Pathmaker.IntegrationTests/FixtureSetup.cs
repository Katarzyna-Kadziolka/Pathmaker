using Microsoft.Extensions.DependencyInjection;
using Pathmaker.Persistence;

namespace Pathmaker.IntegrationTests;

[SetUpFixture]
public class FixtureSetup {
    private static PathmakerApiWebApplicationFactory _applicationFactory = default!;
    public static HttpClient HttpClient = default!;
    public static Func<Task> ResetDatabase = default!;
    public static Func<ApplicationDbContext> GetApplicationDbContext = default!;

    [OneTimeSetUp]
    public async Task OneTimeSetup() {
        _applicationFactory = new PathmakerApiWebApplicationFactory();
        await _applicationFactory.InitializeAsync();

        HttpClient = _applicationFactory.HttpClient;
        ResetDatabase = _applicationFactory.ResetDatabase;
        GetApplicationDbContext = CreateApplicationDbContext;
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown() {
        await _applicationFactory.DisposeAsync();
    }

    private static ApplicationDbContext CreateApplicationDbContext() {
        var context = _applicationFactory.Services.GetRequiredService<IDbContextFactory<ApplicationDbContext>>()
            .CreateDbContext();
        context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        return context;
    }
}