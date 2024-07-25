using System.Text.Json.Nodes;

namespace Pathmaker.Api.Extensions;

// ReSharper disable once InconsistentNaming
public static class IConfigurationExtensions {
    public static JsonNode ToJson(this IConfiguration configuration) {
        return Serialize(configuration);
    }

    private static JsonNode Serialize(IConfiguration configuration) {
        JsonObject obj = new();

        foreach (var child in configuration.GetChildren()) {
            if (child.Path.EndsWith(":0")) {
                var arr = new JsonArray();

                foreach (var arrayChild in configuration.GetChildren()) {
                    arr.Add(Serialize(arrayChild));
                }

                return arr;
            }

            obj.Add(child.Key, Serialize(child));
        }

        if (obj.Count == 0 && configuration is IConfigurationSection section) {
            if (bool.TryParse(section.Value, out bool boolean)) {
                return JsonValue.Create(boolean);
            }

            if (decimal.TryParse(section.Value, out decimal real)) {
                return JsonValue.Create(real);
            }

            if (long.TryParse(section.Value, out long integer)) {
                return JsonValue.Create(integer);
            }

            return JsonValue.Create(section.Value ?? "");
        }

        return obj;
    }
}
