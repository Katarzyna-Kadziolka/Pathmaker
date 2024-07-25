using System.Text.Json;

namespace Pathmaker.Api.Extensions;

public static class WebApplicationExtensions {
    public static void MapDebugEndpoints(this WebApplication app) {
        app.MapGet("/debug/configuration",
            (IConfiguration configuration) => {
                var json = configuration.ToJson();
                return json.ToJsonString(new JsonSerializerOptions { WriteIndented = true });
            });
    }
}
