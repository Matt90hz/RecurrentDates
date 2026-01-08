using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace IncaTechnologies.Recurrence
{
    /// <summary>
    /// Convert an <see cref="IRecurrent"/> from and to Json.
    /// </summary>
    public class RecurrenceJsonConverter : JsonConverter<IRecurrent>
    {
        /// <inheritdoc/>
        public override IRecurrent Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return Recurrence.JsonParser.Parse(ref reader);
        }

        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, IRecurrent value, JsonSerializerOptions options)
        {
            value.ToJsonObject().WriteTo(writer, options);
        }
    }
}