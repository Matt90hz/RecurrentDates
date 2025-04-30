using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Globalization;

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
            foreach(var occurrence in weekliesOrDailies)
            {
                if(int.TryParse(occurrence.Key, out var day))
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