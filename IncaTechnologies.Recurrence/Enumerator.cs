using System;
using System.Collections.Generic;
using System.Linq;

namespace IncaTechnologies.Recurrence
{
    /// <summary>
    /// Contains functions to enumerate a <see cref="IRecurrent"/> object.
    /// </summary>
    public static class Enumerator
    {
        /// <summary>
        /// Enumerates all the occurrences of a recurrence from the given starting date (<paramref name="from"/>) to the given end date (<paramref name="to"/>).
        /// </summary>
        /// <param name="recurrence">The recurrence definition to enumerate.</param>
        /// <param name="from">Starting date.</param>
        /// <param name="to">End date.</param>
        /// <returns>All the occurrences of the recurrence.</returns>
        /// <exception cref="ArgumentException">The <paramref name="from"/> argument must be before or the same date as the <paramref name="to"/> argument.</exception>
        public static IEnumerable<DateTime> AsEnumerable(this IRecurrent recurrence, DateTime from, DateTime to)
        {
            if (from > to)
            {
                throw new ArgumentException($"Invalid period, from: {from} comes after to: {to}.", nameof(to));
            }

            var ancestor = recurrence.GetRoot();

            if (ancestor is IDaily daily)
            {
                return daily.AsEnumerable(from, to);
            }
            else if (ancestor is IWeekly weekly)
            {
                return weekly.AsEnumerable(from, to);
            }
            else if (ancestor is IMonthly monthly)
            {
                return monthly.AsEnumerable(from, to);
            }
            else if (ancestor is IYearly yearly)
            {
                return yearly.AsEnumerable(from, to);
            }
            else
            {
                return Enumerable.Empty<DateTime>();
            }
        }
        private static IEnumerable<DateTime> AsEnumerable(this IDaily daily, DateTime from, DateTime to)
            => GetDatesBetween(from, to)
                .SelectMany(date => daily.SelectAt(x => new DateTime(date.Year, date.Month, date.Day, x.Hour, x.Minute, x.Second)));
        private static IEnumerable<DateTime> AsEnumerable(this IWeekly weekly, DateTime from, DateTime to)
            => GetDatesBetween(from, to)
                .SelectMany(date => weekly.SelectOn(on => (date, on.DayOfWeek, on.Then)))
                .Where(x => x.date.DayOfWeek == x.DayOfWeek)
                .SelectMany(x => x.Then.AsEnumerable(x.date, x.date));
        private static IEnumerable<DateTime> AsEnumerable(this IMonthly monthly, DateTime from, DateTime to) 
            => GetDatesBetween(from, to)
                .SelectMany(date => monthly.SelectThe(
                    x => (Occurs: x.DayOfMonth == date.Day, date, x.Then),
                    x => (Occurs: date.DayOfWeek == x.DayOfWeek && date.GetDayInMonth().Contains(x.DayInMonth), date, x.Then)))
                .Where(x => x.Occurs)
                .SelectMany(x => x.Then.AsEnumerable(x.date, x.date));
        private static IEnumerable<DateTime> AsEnumerable(this IYearly yearly, DateTime from, DateTime to)
            => GetDatesBetween(from, to)
                .SelectMany(date => yearly.SelectIn(x => (x.Month, date, x.Then)))
                .Where(x => x.date.Month == x.Month)
                .SelectMany(x => x.Then.AsEnumerable(x.date, x.date));
        private static IEnumerable<DateTime> GetDatesBetween(DateTime from, DateTime to) 
            => Enumerable.Range(0, (to - from).Days + 1)
                .Select(offset => from.AddDays(offset));
        private static IEnumerable<DayInMonth> GetDayInMonth(this DateTime date)
        {
            var daysInMonth = Enumerable.Range(1, DateTime.DaysInMonth(date.Year, date.Month))
                .Select(day => new DateTime(date.Year, date.Month, day))
                .Where(x => x.DayOfWeek == date.DayOfWeek)
                .Select((x, i) => (x.Day, DayInMonth: (DayInMonth)i));

            var dayInMonth = daysInMonth.First(x => x.Day == date.Day).DayInMonth;

            yield return dayInMonth;

            if (dayInMonth == DayInMonth.Fourth && daysInMonth.Last().DayInMonth == DayInMonth.Fourth)
            {
                yield return DayInMonth.Last;
            }
        }
    }
}