using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace IncaTechnologies.Recurrence
{
    public class RecurrenceJsonConverter : JsonConverter<IRecurrent>
    {
        public override IRecurrent Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return Recurrence.JsonParser.Parse(ref reader);
        }

        public override void Write(Utf8JsonWriter writer, IRecurrent value, JsonSerializerOptions options)
        {
            
        }
    }
}
