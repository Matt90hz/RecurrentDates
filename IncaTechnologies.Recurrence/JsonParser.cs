using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
        /// <returns>True, if the parsing is successful. False, if an exception is thrown during the parsing.</returns>
        public static bool TryParse(string recurrence, out IRecurrent result)
        {
            try
            {
                result = Parse(recurrence);
                return true;
            }
            catch (Exception)
            {
                result = null;
                return false;
            }
        }

        /// <summary>
        /// Parse a Json string into a recurrent object.
        /// </summary>
        /// <param name="recurrence">Json string.</param>
        /// <returns><see cref="IRecurrent"/> representation of the recurrence.</returns>
        /// <exception cref="ArgumentException">The Json must contain valid recurrence keys.</exception>
        public static IRecurrent Parse(string recurrence)
        {
            var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes(recurrence));
            return Parse(ref reader).GetRoot();

            var jsonObject = JsonNode.Parse(recurrence).AsObject();

            if (jsonObject.ContainsKey(JsonSerializer.DAILY_KEY))
            {
                return ParseDaily(jsonObject[JsonSerializer.DAILY_KEY]);
            }
            else if (jsonObject.ContainsKey(JsonSerializer.WEEKLY_KEY))
            {
                return ParseWeekly(jsonObject[JsonSerializer.WEEKLY_KEY]);
            }
            else if (jsonObject.ContainsKey(JsonSerializer.MONTHLY_KEY))
            {
                return ParseMonthly(jsonObject[JsonSerializer.MONTHLY_KEY]);
            }
            else if (jsonObject.ContainsKey(JsonSerializer.YEARLY_KEY))
            {
                return ParseYearly(jsonObject[JsonSerializer.YEARLY_KEY]);
            }
            else
            {
                throw new ArgumentException($"Impossible to parse the Json.\n{recurrence}");
            }
        }

        /// <summary>
        /// Parse a Json stream to a recurrent object.
        /// </summary>
        /// <param name="reader">Stream.</param>
        /// <returns><see cref="IRecurrent"/> representation of the recurrence.</returns>
        /// <exception cref="JsonException"></exception>
        public static IRecurrent Parse(ref Utf8JsonReader reader)
        {
            while (reader.Read())
            {
                if (reader.TokenType is JsonTokenType.PropertyName && reader.GetString() is string name)
                {
                    if (name is JsonSerializer.YEARLY_KEY)
                    {
                        return ParseYearly(ref reader, Occurs.EveryYear());
                    }
                    else if (name is JsonSerializer.MONTHLY_KEY)
                    {
                        return ParseMonthly(ref reader, Occurs.EveryMonth());
                    }
                    else if (name is JsonSerializer.WEEKLY_KEY)
                    {
                        return ParseWeekly(ref reader, Occurs.EveryWeek());
                    }
                    else if (name is JsonSerializer.DAILY_KEY)
                    {
                        return ParseDaily(ref reader, Occurs.EveryDay());
                    }
                    else
                    {
                        throw new JsonException("Unexpected opening property to parse IRecurrent.");
                    }
                }
            }

            throw new JsonException("Not found any property to start IRecurrent parsing.");
        }


        private static readonly IReadOnlyDictionary<string, int> _monthNumber = new Dictionary<string, int>()
        {
            ["January"] = 1,
            ["February"] = 2,
            ["March"] = 3,
            ["April"] = 4,
            ["May"] = 5,
            ["June"] = 6,
            ["July"] = 7,
            ["August"] = 8,
            ["September"] = 9,
            ["October"] = 10,
            ["November"] = 11,
            ["December"] = 12,
        };

        private static IRecurrent ParseYearly(ref Utf8JsonReader reader, IMonthly yearly)
        {
            int inDepth = 0;

            while (reader.Read() && reader.CurrentDepth >= inDepth)
            {
                if (reader.TokenType is JsonTokenType.PropertyName && reader.GetString() is string name)
                {
                    if (name is JsonSerializer.YEARLY_IN_KEY)
                    {
                        inDepth = reader.CurrentDepth;
                        continue;
                    }

                    if (_monthNumber.TryGetValue(name, out var monthNum))
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
                    if (name is JsonSerializer.MONTHLY_THE_KEY)
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
                    if (name is JsonSerializer.WEEKLY_ON_KEY)
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
                    if (name is JsonSerializer.DAILY_AT_KEY)
                    {
                        atDepth = reader.CurrentDepth;
                        continue;
                    }

                    if (name is JsonSerializer.HOURLY_KEY)
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

        private static Daily ParseDaily(JsonNode node)
        {
            var daily = new Daily();
            var times = node[JsonSerializer.DAILY_AT_KEY].AsObject().Select(x => x.Value);
            foreach (var time in times)
            {
                var hourly = new Hourly();
                var minutely = new Minutely();
                var secondly = new Secondly();

                hourly.Hour = time[JsonSerializer.HOURLY_KEY].GetValue<int>();
                hourly.Daily = daily;
                hourly.Minutely = minutely;
                minutely.Minute = time[JsonSerializer.MINUTELY_KEY].GetValue<int>();
                minutely.Hourly = hourly;
                minutely.Secondly = secondly;
                secondly.Second = time[JsonSerializer.SECONDLY_KEY].GetValue<int>();
                secondly.Minutely = minutely;

                daily.At.Add(hourly);
            }

            return daily;
        }

        private static Weekly ParseWeekly(JsonNode node)
        {
            var weekly = new Weekly();
            var days = node[JsonSerializer.WEEKLY_ON_KEY].AsObject();
            foreach (var day in days)
            {
                var daily = ParseDaily(day.Value);

                daily.DayOfWeek = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), day.Key);
                daily.Weekly = weekly;

                weekly.On.Add(daily);
            }

            return weekly;
        }

        private static Monthly ParseMonthly(JsonNode node)
        {
            var monthly = new Monthly();
            var weekliesOrDailies = node[JsonSerializer.MONTHLY_THE_KEY].AsObject();

            foreach (var occurrence in weekliesOrDailies)
            {
                if (int.TryParse(occurrence.Key, out var day))
                {
                    var daily = ParseDaily(occurrence.Value);
                    daily.DayOfMonth = day;
                    daily.Monthly = monthly;

                    monthly.TheDay.Add(daily);
                }
                else
                {
                    var daily = ParseDaily(occurrence.Value);
                    var keySplit = occurrence.Key.Split('-');
                    daily.DayInMonth = (DayInMonth)Enum.Parse(typeof(DayInMonth), keySplit[0]);
                    daily.DayOfWeek = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), keySplit[1]);
                    daily.Monthly = monthly;

                    monthly.InDay.Add(daily);
                }
            }

            return monthly;
        }

        private static Yearly ParseYearly(JsonNode node)
        {
            var yearly = new Yearly();
            var occurrences = node[JsonSerializer.YEARLY_IN_KEY].AsObject();
            var enCulture = CultureInfo.GetCultureInfo("en");
            foreach (var occurrence in occurrences)
            {
                var monthly = ParseMonthly(occurrence.Value);
                monthly.Month = DateTime.ParseExact(occurrence.Key, "MMMM", enCulture).Month;
                monthly.Yearly = yearly;

                yearly.In.Add(monthly);
            }

            return yearly;
        }
    }
}