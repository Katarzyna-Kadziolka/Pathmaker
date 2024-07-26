using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Pathmaker.Shared.Features;

namespace Pathmaker.Infrastructure.Extensions;

public static class WebApplicationBuilderExtensions {
    public static void AddSentry(this WebApplicationBuilder builder) {
        if (builder.Configuration.IsFeatureEnabled("Sentry")) {
            builder.WebHost.UseSentry();
        }
    }
}
