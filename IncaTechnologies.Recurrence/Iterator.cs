using System;

namespace IncaTechnologies.Recurrence
{
    /// <summary>
    /// Contains functions to iterate a <see cref="IRecurrent"/> data structure.
    /// </summary>
    public static class Iterator
    {
        /// <summary>
        /// Iterates through every monthly occurrence of a yearly recurrence. 
        /// </summary>
        /// <param name="yearly">Recurrence.</param>
        /// <param name="action">Action.</param>
        /// <returns>The <paramref name="yearly"/> parameter.</returns>
        public static IYearly ForEachIn(this IYearly yearly, Action<(int Month, IMonthly Then)> action)
        {
            foreach (var monthly in yearly.GetIn())
            {
                action((monthly.Month, monthly));
            }

            return yearly;
        }
        /// <summary>
        /// Iterates through every daily occurrence of a monthly recurrence.
        /// </summary>
        /// <param name="monthly">Recurrence.</param>
        /// <param name="action1">Action on the day of the month.</param>
        /// <param name="action2">Action on the day in the month.</param>
        /// <returns>The <paramref name="monthly"/> parameter.</returns>
        public static IMonthly ForEachThe(
            this IMonthly monthly, 
            Action<(int DayOfMonth, IDaily Then)> action1, 
            Action<(DayInMonth DayInMonth, DayOfWeek DayOfWeek, IDaily Then)> action2)
        {
            foreach (var daily in monthly.GetThe())
            {
                if (daily.DayOfMonth != 0)
                {
                    action1((daily.DayOfMonth, daily));
                }
                else 
                {
                    action2((daily.DayInMonth, daily.DayOfWeek, daily));
                }
            }

            return monthly;
        }
        /// <summary>
        /// Iterates through every day of the month occurrence in a monthly recurrence.
        /// </summary>
        /// <param name="monthly">Recurrence.</param>
        /// <param name="action">Action.</param>
        /// <returns>The <paramref name="monthly"/> parameter.</returns>
        public static IMonthly ForEachTheDay(this IMonthly monthly, Action<(int DayOfMonth, IDaily Then)> action)
        {
            foreach (var daily in monthly.GetTheDay())
            {
                action((daily.DayOfMonth, daily));         
            }

            return monthly;
        }
        /// <summary>
        /// Iterates through every week day in the month occurrence in a monthly recurrence.
        /// </summary>
        /// <param name="monthly">Recurrence.</param>
        /// <param name="action">Action.</param>
        /// <returns>The <paramref name="monthly"/> parameter.</returns>
        public static IMonthly ForEachTheWeekDay(this IMonthly monthly, Action<(DayInMonth DayInMonth, DayOfWeek DayOfWeek, IDaily Then)> action)
        {
            foreach (var daily in monthly.GetTheWeekDay())
            {
                action((daily.DayInMonth, daily.DayOfWeek, daily));
            }

            return monthly;
        }
        /// <summary>
        /// Iterates through every daily occurrence in a weekly recurrence.
        /// </summary>
        /// <param name="weekly">Recurrence.</param>
        /// <param name="action">Action.</param>
        /// <returns>The <paramref name="weekly"/> parameter.</returns>
        public static IWeekly ForEachOn(this IWeekly weekly, Action<(DayOfWeek DayOfWeek, IDaily Then)> action)
        {
            foreach (var daily in weekly.GetOn())
            {
                action((daily.DayOfWeek, daily));
            }

            return weekly;
        }
        /// <summary>
        /// Iterates through every time of the day occurrence of a daily recurrence.
        /// </summary>
        /// <param name="daily">Recurrence.</param>
        /// <param name="action">Action.</param>
        /// <returns>The <paramref name="daily"/> parameter.</returns>
        public static IDaily ForEachAt(this IDaily daily, Action<(int Hour, int Minute, int Second)> action)
        {
            foreach (var hourly in daily.GetAt())
            {
                action((hourly.Hour, hourly.Minutely.Minute, hourly.Minutely.Secondly.Second));
            }

            return daily;
        }
    }
}
