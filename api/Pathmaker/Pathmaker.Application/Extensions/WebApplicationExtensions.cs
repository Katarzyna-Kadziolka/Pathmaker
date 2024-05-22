using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pathmaker.Persistence;

namespace Pathmaker.Application.Extensions;

public static class WebApplicationExtensions {
    public static void AddMigration(this WebApplication app) {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
        }
        catch {
            // ignored
        }
    }
}
