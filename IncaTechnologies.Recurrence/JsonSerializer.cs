using System.Text.Json;
using System;
using System.Text.Json.Nodes;
using System.Linq;
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
            => recurrence.ToJsonObject().ToJsonString(options);

        internal static JsonObject ToJsonObject(this IRecurrent recurrent)
        {
            var ancestor = recurrent.GetRoot();

            if (ancestor is IDaily daily)
            {
                return new JsonObject { [DAILY_KEY] = daily.ToJsonObject() };
            }
            else if (ancestor is IWeekly weekly)
            {
                return new JsonObject { [WEEKLY_KEY] = weekly.ToJsonObject() };
            }
            else if (ancestor is IMonthly monthly)
            {
                return new JsonObject { [MONTHLY_KEY] = monthly.ToJsonObject() };
            }
            else if (ancestor is IYearly yearly)
            {
                return new JsonObject { [YEARLY_KEY] = yearly.ToJsonObject() };
            }
            else
            {
                throw new Exception($"Ancestor not found. Type evaluated: '{recurrent.GetType().Name}'.");
            }
        }

        private static JsonObject ToJsonObject(this IDaily daily)
        {
            var occurrences = daily
                .SelectAt(x => new JsonObject
                {
                    [HOURLY_KEY] = x.Hour,
                    [MINUTELY_KEY] = x.Minute,
                    [SECONDLY_KEY] = x.Second,
                })
                .Select((x, i) => (Key: $"Time{i}", Val: x))
                .Aggregate(new JsonObject(), (json, x) => { json[x.Key] = x.Val; return json; });

            return new JsonObject { [DAILY_AT_KEY] = occurrences };
        }

        private static JsonObject ToJsonObject(this IWeekly weekly)
        {
            var occurrences = weekly
                .SelectOn(x => (Key: x.DayOfWeek.ToFriendlyString(), Val: x.Then.ToJsonObject()))
                .Aggregate(new JsonObject(), (json, x) => { json[x.Key] = x.Val; return json; });

            return new JsonObject { [WEEKLY_ON_KEY] = occurrences };
        }

        private static JsonObject ToJsonObject(this IMonthly monthly)
        {
            var occurrences = monthly
                .SelectThe(
                    x => (Key: x.DayOfMonth.ToString(), Val: x.Then.ToJsonObject()),
                    x => (Key: $"{x.DayInMonth.ToFriendlyString()}-{x.DayOfWeek.ToFriendlyString()}", Val: x.Then.ToJsonObject()))
                .Aggregate(new JsonObject(), (json, x) => { json[x.Key] = x.Val; return json; });

            return new JsonObject { [MONTHLY_THE_KEY] = occurrences };
        }

        private static JsonObject ToJsonObject(this IYearly yearly)
        {
            var monthlyOccurrences = yearly
                .SelectIn(x => (Key: MonthName[x.Month], Val: x.Then.ToJsonObject()))
                .Aggregate(new JsonObject(), (json, x) => { json[x.Key] = x.Val; return json; });

            return new JsonObject { [YEARLY_IN_KEY] = monthlyOccurrences };
        }

        internal static string ToFriendlyString(this DayInMonth dayInMonth) 
            => Enum.GetName(typeof(DayInMonth), dayInMonth);

        internal static string ToFriendlyString(this DayOfWeek dayOfWeek) 
            => Enum.GetName(typeof(DayOfWeek), dayOfWeek);

    }
}