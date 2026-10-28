using System.Collections;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Roy.Logging.Json;

/// <summary>
/// Writes an exception as JSON: type, message, source, help link, HResult, stack trace,
/// data and the inner exception, recursively. System.Text.Json cannot serialize an
/// exception on its own once it has been thrown (TargetSite is a MethodBase).
/// Reading gives the message back as a plain exception.
/// </summary>
public class ExceptionJsonConverter : JsonConverter<Exception>
{
    /// <summary>
    /// Options with this converter, for serializing an exception on its own.
    /// </summary>
    private static readonly JsonSerializerOptions Options = new JsonSerializerOptions()
    {
        Converters = { new ExceptionJsonConverter() }
    };

    /// <summary>
    /// Serialize an exception on its own.
    /// </summary>
    /// <param name="exception">
    /// Exception to serialize.
    /// </param>
    /// <returns>
    /// JSON, or null when the exception is null.
    /// </returns>
    public static string? ToJson(Exception? exception)
    {
        if (exception == null)
        {
            return null;
        }

        return JsonSerializer.Serialize(exception, Options);
    }

    /// <inheritdoc/>
    public override Exception? Read(ref Utf8JsonReader reader, Type typeToConvert,
        JsonSerializerOptions options)
    {
        using (JsonDocument document = JsonDocument.ParseValue(ref reader))
        {
            JsonElement root = document.RootElement;
            if (root.ValueKind != JsonValueKind.Object)
            {
                return null;
            }
            string? message = root.TryGetProperty("Message", out JsonElement value)
                ? value.GetString() : null;

            return new Exception(message);
        }
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, Exception value,
        JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("Type", value.GetType().FullName);
        writer.WriteString("Message", value.Message);
        this.WriteIfNotNull(writer, "Source", value.Source);
        this.WriteIfNotNull(writer, "HelpLink", value.HelpLink);
        writer.WriteNumber("HResult", value.HResult);
        this.WriteIfNotNull(writer, "StackTrace", value.StackTrace);
        if (value.Data.Count > 0)
        {
            writer.WritePropertyName("Data");
            writer.WriteStartObject();
            foreach (DictionaryEntry entry in value.Data)
            {
                writer.WriteString(entry.Key.ToString()!, entry.Value?.ToString());
            }
            writer.WriteEndObject();
        }
        if (value.InnerException != null)
        {
            writer.WritePropertyName("InnerException");
            this.Write(writer, value.InnerException, options);
        }
        writer.WriteEndObject();
    }

    /// <summary>
    /// Write a string property when it has a value.
    /// </summary>
    private void WriteIfNotNull(Utf8JsonWriter writer, string name, string? value)
    {
        if (value != null)
        {
            writer.WriteString(name, value);
        }
    }
}
