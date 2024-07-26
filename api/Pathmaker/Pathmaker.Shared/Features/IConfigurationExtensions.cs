using Microsoft.Extensions.Configuration;

namespace Pathmaker.Shared.Features;

// ReSharper disable once InconsistentNaming
public static class IConfigurationExtensions {
    public static bool IsFeatureEnabled(this IConfiguration configuration, string featureName) {
        var options = configuration.GetSection(FeatureFlags.SectionName);
        var feature = options.GetSection(featureName).Get<bool>();
        return feature;
    }
}
