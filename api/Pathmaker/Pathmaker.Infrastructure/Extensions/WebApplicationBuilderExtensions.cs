using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace Pathmaker.Infrastructure.Extensions;

public static class WebApplicationBuilderExtensions {
    public static void AddSentry(this WebApplicationBuilder builder) {
        builder.WebHost.UseSentry();
    }
}
