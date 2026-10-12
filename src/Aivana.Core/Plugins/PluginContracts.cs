using System.Text.Json;
using System.IO.Pipes;
using System.Text;
namespace Aivana.Core.Plugins;
public sealed record PluginAction(string Id, string Name, string ParameterSchema);
public sealed record PluginManifest(string Id, string Version, string EntryPoint, PluginAction[] Actions, string[] Capabilities, Dictionary<string,string> Hashes, int ProtocolVersion = 1, string[]? Dependencies = null);
public sealed record PluginInvocation(string PackageId, string ActionId, JsonElement Parameters, int TimeoutMs = 10000);
public sealed record PluginMessage(int Version, Guid Id, string Method, JsonElement Parameters, bool Success = true, string Error = "");
public static class PluginWire
{
    public const int MaximumBytes = 1024 * 1024;
    public static async Task WriteAsync(Stream stream, PluginMessage message, CancellationToken token)
    {
        var bytes = JsonSerializer.SerializeToUtf8Bytes(message);
        if (bytes.Length > MaximumBytes) throw new InvalidDataException("Plugin message too large.");
        await stream.WriteAsync(BitConverter.GetBytes(bytes.Length), token); await stream.WriteAsync(bytes, token); await stream.FlushAsync(token);
    }
    public static async Task<PluginMessage> ReadAsync(Stream stream, CancellationToken token)
    {
        var header = new byte[4]; await stream.ReadExactlyAsync(header, token);
        var length = BitConverter.ToInt32(header);
        if (length is < 1 or > MaximumBytes) throw new InvalidDataException("Invalid plugin message size.");
        var bytes = new byte[length]; await stream.ReadExactlyAsync(bytes, token);
        var message = JsonSerializer.Deserialize<PluginMessage>(bytes) ?? throw new InvalidDataException("Missing plugin message.");
        if (message.Version != 1 || message.Id == Guid.Empty) throw new InvalidDataException("Unsupported plugin protocol.");
        return message;
    }
}
public static class PluginParameters
{
    public static void Validate(string schema, JsonElement value)
    {
        using var document = JsonDocument.Parse(schema);
        var root = document.RootElement;
        Check(root, value, "parameters");
    }
    private static void Check(JsonElement schema, JsonElement value, string name)
    {
        if (schema.TryGetProperty("type", out var type))
        {
            var matches = type.GetString() switch { "object" => value.ValueKind == JsonValueKind.Object, "string" => value.ValueKind == JsonValueKind.String, "integer" => value.TryGetInt64(out _), "number" => value.ValueKind == JsonValueKind.Number, "boolean" => value.ValueKind is JsonValueKind.True or JsonValueKind.False, _ => false };
            if (!matches) throw new InvalidDataException("Invalid type: " + name);
        }
        if (schema.TryGetProperty("enum", out var options) && !options.EnumerateArray().Any(o => o.GetRawText() == value.GetRawText())) throw new InvalidDataException("Invalid enum: " + name);
        if (value.ValueKind == JsonValueKind.Object)
        {
            if (schema.TryGetProperty("required", out var required)) foreach (var item in required.EnumerateArray()) if (!value.TryGetProperty(item.GetString()!, out _)) throw new InvalidDataException("Missing parameter: " + item);
            foreach (var property in value.EnumerateObject())
            {
                if (schema.TryGetProperty("properties", out var properties) && properties.TryGetProperty(property.Name, out var child)) Check(child, property.Value, property.Name);
                else if (schema.TryGetProperty("additionalProperties", out var additional) && additional.ValueKind == JsonValueKind.False) throw new InvalidDataException("Unknown parameter: " + property.Name);
            }
        }
        if (value.ValueKind == JsonValueKind.Number)
        {
            var number = value.GetDouble();
            if (schema.TryGetProperty("minimum", out var min) && number < min.GetDouble() || schema.TryGetProperty("maximum", out var max) && number > max.GetDouble()) throw new InvalidDataException("Parameter out of range: " + name);
        }
    }
}
