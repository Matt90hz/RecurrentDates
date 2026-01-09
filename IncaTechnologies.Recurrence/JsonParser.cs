using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static JsonPropertyNames;

namespace IncaTechnologies.Recurrence
{
    /// <summary>
    /// Contains functions to parse a JSON text to a <see cref="IRecurrent"/> object.
    /// </summary>
    public static class JsonParser
    {
        /// <summary>
        /// Try to parse a Json string into a recurrent object.
        /// </summary>
        /// <param name="recurrence">Json string.</param>
        /// <param name="result">The resulting recurrence or null in case of failure.</param>
        /// <param name="options">Reader options. Only <see cref="JsonSerializerOptions.AllowTrailingCommas"/> and <see cref="JsonSerializerOptions.MaxDepth"/> are considered.</param>
        /// <returns>True, if the parsing is successful. False, if an exception is thrown during the parsing.</returns>
        public static bool TryParse(string recurrence, out IRecurrent result, JsonSerializerOptions options = null)
        {
            try
            {
                result = Parse(recurrence, options);
                return true;
            }
            catch (JsonException)
            {
                result = null;
                return false;
            }
        }

        /// <summary>
        /// Parse a Json string into a recurrent object.
        /// </summary>
        /// <param name="recurrence">Json string.</param>
        /// <param name="options">Reader options. Only <see cref="JsonSerializerOptions.AllowTrailingCommas"/> and <see cref="JsonSerializerOptions.MaxDepth"/> are considered.</param>
        /// <returns><see cref="IRecurrent"/> representation of the recurrence.</returns>
        /// <exception cref="JsonException">The Json must contain valid recurrence keys.</exception>
        public static IRecurrent Parse(string recurrence, JsonSerializerOptions options = null)
        {
            options = options ?? JsonSerializerOptions.Web;

            var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes(recurrence), options.ToJsonReaderOptions());

            return Parse(ref reader);
        }

        /// <summary>
        /// Parse a Json stream to a recurrent object.
        /// </summary>
        /// <param name="reader">Stream.</param>
        /// <returns><see cref="IRecurrent"/> representation of the recurrence.</returns>
        /// <exception cref="JsonException"></exception>
        public static IRecurrent Parse(ref Utf8JsonReader reader)
        {
            IRecurrent recurrent = null;
            
            while (reader.Read())
            {
                if (reader.TokenType is JsonTokenType.EndObject) break;

                if (reader.TokenType is JsonTokenType.PropertyName && reader.GetString() is string name)
                {
                    if (name is YEARLY_KEY)
                    {
                        recurrent = ParseYearly(ref reader, Occurs.EveryYear());
                    }
                    else if (name is MONTHLY_KEY)
                    {
                        recurrent = ParseMonthly(ref reader, Occurs.EveryMonth());
                    }
                    else if (name is WEEKLY_KEY)
                    {
                        recurrent = ParseWeekly(ref reader, Occurs.EveryWeek());
                    }
                    else if (name is DAILY_KEY)
                    {
                        recurrent = ParseDaily(ref reader, Occurs.EveryDay());
                    }
                    else
                    {
                        throw new JsonException($"Unexpected opening property to parse IRecurrent. Found: {name}.");
                    }
                }
            }

            return recurrent is null 
                ? throw new JsonException("Not found any property to start IRecurrent parsing.")
                : recurrent.GetRoot();
        }

        private static IRecurrent ParseYearly(ref Utf8JsonReader reader, IMonthly yearly)
        {
            int inDepth = 0;

            while (reader.Read() && reader.CurrentDepth >= inDepth)
            {
                if (reader.TokenType is JsonTokenType.PropertyName && reader.GetString() is string name)
                {
                    if (name is YEARLY_IN_KEY)
                    {
                        inDepth = reader.CurrentDepth;
                        continue;
                    }

                    if (MonthNumber.TryGetValue(name, out var monthNum))
                    {
                        ParseMonthly(ref reader, yearly.Month(monthNum));
                    }
                }
            }

            return yearly;
        }

        private static IRecurrent ParseMonthly(ref Utf8JsonReader reader, IWeekly monthly)
        {
            int theDepth = 0;

            while (reader.Read() && reader.CurrentDepth >= theDepth)
            {
                if (reader.TokenType is JsonTokenType.PropertyName && reader.GetString() is string name)
                {
                    if (name is MONTHLY_THE_KEY)
                    {
                        theDepth = reader.CurrentDepth;
                        continue;
                    }

                    if (int.TryParse(name, out var day))
                    {
                        ParseDaily(ref reader, monthly.Day(day));
                        continue;
                    }

                    if (name.Split('-') is string[] enums
                        && enums.Length == 2
                        && Enum.TryParse<DayInMonth>(enums[0], out var dayInMonth)
                        && Enum.TryParse<DayOfWeek>(enums[1], out var dayOfWeek))
                    {
                        ParseDaily(ref reader, monthly.SetDayInMonth(dayInMonth, dayOfWeek));
                    }
                }
            }

            return monthly;
        }

        private static IRecurrent ParseWeekly(ref Utf8JsonReader reader, IDaily weekly)
        {
            int onDepth = 0;

            while (reader.Read() && reader.CurrentDepth >= onDepth)
            {
                if (reader.TokenType is JsonTokenType.PropertyName && reader.GetString() is string name)
                {
                    if (name is WEEKLY_ON_KEY)
                    {
                        onDepth = reader.CurrentDepth;
                        continue;
                    }

                    if (Enum.TryParse<DayOfWeek>(name, out var dayOfWeek))
                    {
                        ParseDaily(ref reader, weekly.SetDayOfWeek(dayOfWeek));
                    }
                }
            }

            return weekly;
        }

        private static IRecurrent ParseDaily(ref Utf8JsonReader reader, IHourly daily)
        {
            int atDepth = 0;

            while (reader.Read() && reader.CurrentDepth >= atDepth)
            {
                if (reader.TokenType is JsonTokenType.PropertyName && reader.GetString() is string name)
                {
                    if (name is DAILY_AT_KEY)
                    {
                        atDepth = reader.CurrentDepth;
                        continue;
                    }

                    if (name is HOURLY_KEY)
                    {
                        reader.Read();
                        var hour = reader.GetInt32();
                        reader.Read();
                        reader.Read();
                        var minute = reader.GetInt32();
                        reader.Read();
                        reader.Read();
                        var second = reader.GetInt32();

                        daily.Hour(hour).Minute(minute).Second(second);
                    }
                }
            }

            return daily;
        }

        internal static JsonReaderOptions ToJsonReaderOptions(this JsonSerializerOptions options) => new JsonReaderOptions
        {
            AllowMultipleValues = false,
            AllowTrailingCommas = options.AllowTrailingCommas,
            CommentHandling = JsonCommentHandling.Skip,
            MaxDepth = options.MaxDepth,
        };
    }
}