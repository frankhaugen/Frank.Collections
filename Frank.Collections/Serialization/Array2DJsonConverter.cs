using System.Text.Json;
using System.Text.Json.Serialization;

using Frank.Collections.Extensions;
using Frank.Collections.Multidimensional;

namespace Frank.Collections.Serialization;

public class Array2DJsonConverter<T> : JsonConverter<Array2D<T?>>
{
    public override Array2D<T?> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var array2D = new List<List<T>>();
        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndArray)
                break;

            var row = new List<T>();
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndArray)
                    break;

                var value = JsonSerializer.Deserialize<T>(ref reader, options);
                if (value != null) row.Add(value);
            }
            array2D.Add(row);
        }
        return array2D.To2DArray().ToArray2D();
    }

    public override void Write(Utf8JsonWriter writer, Array2D<T?> value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();

        var array = value.ToUnsafeArray();
        for (var index0 = 0; index0 < array.GetLength(0); index0++)
        {
            writer.WriteStartArray();
            for (var index1 = 0; index1 < array.GetLength(1); index1++)
            {
                var item = array[index0, index1];
                if (item != null)
                {
                    JsonSerializer.Serialize(writer, item, options);
                }
            }
            writer.WriteEndArray();
        }

        writer.WriteEndArray();
    }
}