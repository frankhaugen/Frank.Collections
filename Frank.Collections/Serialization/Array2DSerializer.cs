using System.Globalization;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

using Frank.Collections.Extensions;
using Frank.Collections.Multidimensional;

namespace Frank.Collections.Serialization;

/// <summary>
/// Class for serializing and deserializing array2Ds of type T using a grid-like format.
/// </summary>
public static class Array2DSerializer
{
    /// <summary>
    /// Gets or sets the options for JSON serialization and deserialization. Applied to any item in the array that can be serialized or deserialized as JSON.
    /// </summary>
    public static JsonSerializerOptions Options { get; } = new()
    {
        WriteIndented = false,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true,
        ReadCommentHandling = JsonCommentHandling.Skip,
        Converters = { new JsonStringEnumConverter() },
        ReferenceHandler = ReferenceHandler.IgnoreCycles,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    /// <summary>
    /// Serializes the specified array2D to a JSON string.
    /// </summary>
    /// <param name="sourceArray2D"></param>
    /// <typeparam name="T"></typeparam>
    /// <example>
    /// <code>
    /// [] [2] [3]
    /// [4] [5] [6]
    /// [7] [8] [9]
    /// </code>
    /// </example>
    /// <returns>A string representing the specified array2D.</returns>
    public static string Serialize<T>(Array2D<T> sourceArray2D)
        => sourceArray2D.ToUnsafeArray().Select2D(value => value == null ? null : JsonSerializer.Serialize(value, Options)).AsString();

    public static Array2D<T?> Deserialize<T>(string stringGrid) =>
        stringGrid
            .Split('\n')
            .Select(line => line
                .Split("] [")
                .Select(cell => cell.Trim('[', ']'))
                .Select(cell => cell.Replace("\\\"", "\""))
                .Select(cell => cell.Replace("\"", ""))
                .ToArray())
            .ToArray()
            .To2DArray()
            .Select2D(DeserializeInternal<T>)
            .ToArray2D();

    private static T? DeserializeInternal<T>(string? value)
    {
        if (value == null)
            return default;
        
        var type = typeof(T);
        
        if (type == typeof(string))
            return (T?)(object)value;
        
        if (type.IsEnum)
            return Enum.TryParse(type, value, out var result) ? (T?)(object)result : default;

        // Number types
        if (type == typeof(int) || type == typeof(int?))
            return int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var result) ? (T?)(object?)result : default;
        if (type == typeof(uint) || type == typeof(uint?))
            return uint.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var result) ? (T?)(object?)result : default;
        if (type == typeof(long) || type == typeof(long?))
            return long.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var result) ? (T?)(object?)result : default;
        if (type == typeof(ulong) || type == typeof(ulong?))
            return ulong.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var result) ? (T?)(object?)result : default;
        if (type == typeof(float) || type == typeof(float?))
            return float.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var result) ? (T?)(object?)result : default;
        if (type == typeof(double) || type == typeof(double?))
            return double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var result) ? (T?)(object?)result : default;
        if (type == typeof(decimal) || type == typeof(decimal?))
            return decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var result) ? (T?)(object?)result : default;
        if (type == typeof(BigInteger) || type == typeof(BigInteger?))
            return BigInteger.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var result) ? (T?)(object?)result : default;

        if (type == typeof(bool))
            return bool.TryParse(value, out var result) ? (T?)(object)result : default;
        
        if (type == typeof(char))
            return char.TryParse(value, out var result) ? (T?)(object)result : default;
        
        if (type == typeof(DateTime) || type == typeof(DateTime?))
            return DateTime.TryParse(value, out var result) ? (T?)(object)result : default;
        
        if (type == typeof(DateTimeOffset) || type == typeof(DateTimeOffset?))
            return DateTimeOffset.TryParse(value, out var result) ? (T?)(object)result : default;
        
        if (type == typeof(TimeSpan) || type == typeof(TimeSpan?))
            return TimeSpan.TryParse(value, out var result) ? (T?)(object)result : default;
        
        if (type == typeof(Guid) || type == typeof(Guid?))
            return Guid.TryParse(value, out var result) ? (T?)(object)result : default;
        
        if (type == typeof(Uri))
            return Uri.TryCreate(value, UriKind.RelativeOrAbsolute, out var result) ? (T?)(object)result : default;
        
        if (type == typeof(Version))
            return Version.TryParse(value, out var result) ? (T?)(object)result : default;
        
        var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes(value));
        if (JsonDocument.TryParseValue(ref reader, out var jsonDocument))
        {
            var jsonTypeInfo = JsonTypeInfo.CreateJsonTypeInfo(type, Options);

            var isConvertible = jsonTypeInfo.Converter.CanConvert(type);
            
            if (isConvertible)
                return jsonDocument.Deserialize<T>(Options);
        }
        
        return default;
    }
}