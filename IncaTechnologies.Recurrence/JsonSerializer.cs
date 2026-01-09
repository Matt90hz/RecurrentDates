using System;
using System.Buffers;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization.Metadata;
using static JsonPropertyNames;

namespace IncaTechnologies.Recurrence
{
    /// <summary>
    /// Contains functions to serialize a <see cref="IRecurrent"/> object to JSON text.
    /// </summary>
    public static class JsonSerializer
    {
        /// <summary>
        /// Serialize an IRecurrent instance into a Json string.
        /// </summary>
        /// <param name="recurrence">The recurrence to serialize.</param>
        /// <param name="options">Options for the serialization.</param>
        /// <returns>Json string that represents the <paramref name="recurrence"/>.</returns>
        public static string ToJson(this IRecurrent recurrence, JsonSerializerOptions options = null)
        {
            options = options ?? JsonSerializerOptions.Web;

            using (var stream = new MemoryStream())
            using (var writer = new Utf8JsonWriter(stream, options.ToJsonWriterOptions()))
            {
                recurrence.WriteTo(writer);

                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }

        /// <summary>
        /// Writes the <paramref name="recurrence"/> to the <paramref name="writer"/>.
        /// </summary>
        /// <param name="recurrence"><see cref="IRecurrent"/> to serialize.</param>
        /// <param name="writer">Write stream.</param>
        public static void WriteTo(this IRecurrent recurrence, Utf8JsonWriter writer)
        {
            var root = recurrence.GetRoot();

            writer.WriteStartObject();

            if (root is IDaily daily)
            {
                writer.WriteStartObject(DAILY_KEY);
                daily.WriteTo(writer);
            }
            else if (root is IWeekly weekly)
            {
                writer.WriteStartObject(WEEKLY_KEY);
                weekly.WriteTo(writer);
            }
            else if (root is IMonthly monthly)
            {
                writer.WriteStartObject(MONTHLY_KEY);
                monthly.WriteTo(writer);
            }
            else if (root is IYearly yearly)
            {
                writer.WriteStartObject(YEARLY_KEY);
                yearly.WriteTo(writer);
            }
            else
            {
                throw new ArgumentException($"Unexpected root value. Type evaluated: '{recurrence.GetType().Name}'.", nameof(recurrence));
            }

            writer.WriteEndObject();
            writer.WriteEndObject();
            writer.Flush();
        }

        private static void WriteTo(this IDaily daily, Utf8JsonWriter writer)
        {
            writer.WriteStartObject(DAILY_AT_KEY);

            int i = 0;

            daily.ForEachAt(x =>
            {
                writer.WriteStartObject($"Time{i++}");
                writer.WriteNumber(HOURLY_KEY, x.Hour);
                writer.WriteNumber(MINUTELY_KEY, x.Minute);
                writer.WriteNumber(SECONDLY_KEY, x.Second);
                writer.WriteEndObject();
            });

            writer.WriteEndObject();
        }

        private static void WriteTo(this IWeekly weekly, Utf8JsonWriter writer)
        {
            writer.WriteStartObject(WEEKLY_ON_KEY);

            weekly.ForEachOn(x =>
            {
                writer.WriteStartObject(x.DayOfWeek.ToFriendlyString());
                x.Then.WriteTo(writer);
                writer.WriteEndObject();
            });

            writer.WriteEndObject();
        }

        private static void WriteTo(this IMonthly monthly, Utf8JsonWriter writer)
        {
            writer.WriteStartObject(MONTHLY_THE_KEY);

            monthly.ForEachThe(
                x =>
                {
                    writer.WriteStartObject(x.DayOfMonth.ToString());
                    x.Then.WriteTo(writer);
                    writer.WriteEndObject();
                },
                x =>
                {
                    writer.WriteStartObject($"{x.DayInMonth.ToFriendlyString()}-{x.DayOfWeek.ToFriendlyString()}");
                    x.Then.WriteTo(writer);
                    writer.WriteEndObject();
                });

            writer.WriteEndObject();
        }

        private static void WriteTo(this IYearly yearly, Utf8JsonWriter writer)
        {
            writer.WriteStartObject(YEARLY_IN_KEY);

            yearly.ForEachIn(x =>
            {
                writer.WriteStartObject(MonthName[x.Month]);
                x.Then.WriteTo(writer);
                writer.WriteEndObject();
            });

            writer.WriteEndObject();
        }

        internal static string ToFriendlyString(this DayInMonth dayInMonth)
            => Enum.GetName(typeof(DayInMonth), dayInMonth);

        internal static string ToFriendlyString(this DayOfWeek dayOfWeek)
            => Enum.GetName(typeof(DayOfWeek), dayOfWeek);

        internal static JsonWriterOptions ToJsonWriterOptions(this JsonSerializerOptions options) => new JsonWriterOptions
        {
            Encoder = options.Encoder,
            IndentCharacter = options.IndentCharacter,
            Indented = options.WriteIndented,
            IndentSize = options.IndentSize,
            MaxDepth = options.MaxDepth,
            NewLine = options.NewLine,
            SkipValidation = false,
        };

    }
}