using System.Collections.Generic;
using System.Linq;

namespace IncaTechnologies.Recurrence
{
    internal static class Reader
    {
        internal static IEnumerable<Monthly> GetIn(this IYearly recurrence) 
            => recurrence is Yearly yearly && yearly.In.Count > 0 
            ? yearly.In 
            : DefaultOccurrences.Yearly.In;

        internal static IEnumerable<Daily> GetThe(this IMonthly recurrence)
            => recurrence is Monthly monthly && (monthly.TheDay.Count > 0 || monthly.InDay.Count > 0)
            ? monthly.TheDay.Concat(monthly.InDay)
            : DefaultOccurrences.Monthly.TheDay;

        internal static IEnumerable<Daily> GetTheDay(this IMonthly recurrence)
            => recurrence.GetThe().Where(daily => daily.DayOfMonth != 0);

        internal static IEnumerable<Daily> GetTheWeekDay(this IMonthly recurrence)
            => recurrence.GetThe().Where(daily => daily.DayOfMonth == 0);

        internal static IEnumerable<Daily> GetOn(this IWeekly recurrence)
            => recurrence is Weekly weekly && weekly.On.Count > 0
            ? weekly.On
            : DefaultOccurrences.Weekly.On;

        internal static IEnumerable<Hourly> GetAt(this IDaily recurrence)
            => recurrence is Daily daily && daily.At.Count > 0
            ? daily.At.Select(hourly =>
            {
                hourly.Minutely = hourly.Minutely ?? DefaultOccurrences.Minutely;
                hourly.Minutely.Secondly = hourly.Minutely.Secondly ?? DefaultOccurrences.Secondly;
                return hourly;
            })
            : DefaultOccurrences.Daily.At;
    }

}