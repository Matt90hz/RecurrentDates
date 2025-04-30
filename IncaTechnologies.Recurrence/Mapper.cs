using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace IncaTechnologies.Recurrence
{ 
    /// <summary>
    /// Contains function to map a <see cref="IRecurrent"/> data structure to any other form.
    /// </summary>
    public static class Mapper
    {
        /// <summary>
        /// Maps the times occurrences of a daily recurrence to the selected form.
        /// </summary>
        /// <typeparam name="T">Any type.</typeparam>
        /// <param name="daily">Daily recurrence.</param>
        /// <param name="selector">Mapping function.</param>
        /// <returns>Every time occurrence projected by the mapping function.</returns>
        public static IEnumerable<T> SelectAt<T>(this IDaily daily, Func<(int Hour, int Minute, int Second), T> selector) 
            => daily.GetAt().Select(x => selector((x.Hour, x.Minutely.Minute, x.Minutely.Secondly.Second)));
        /// <summary>
        /// Maps the day of the week occurrences of a weekly recurrence to the selected form.
        /// </summary>
        /// <typeparam name="T">Any type.</typeparam>
        /// <param name="weekly">Weekly recurrence.</param>
        /// <param name="selector">Mapping function.</param>
        /// <returns>Every day of the week occurrence projected by the mapping function.</returns>
        public static IEnumerable<T> SelectOn<T>(this IWeekly weekly, Func<(DayOfWeek DayOfWeek, IDaily Then), T> selector) 
            => weekly.GetOn().Select(daily => selector((daily.DayOfWeek, daily)));
        /// <summary>
        /// Maps every daily occurrence of a monthly recurrence, day of the month or day of the week in the month, to the selected form.
        /// </summary>
        /// <typeparam name="T">Any type.</typeparam>
        /// <param name="monthly">Monthly recurrence</param>
        /// <param name="selector1">Maps the day of the month.</param>
        /// <param name="selector2">Maps the day in the month.</param>
        /// <returns>Every daily occurrence projected by the mapping function.</returns>
        public static IEnumerable<T> SelectThe<T>(
            this IMonthly monthly, 
            Func<(int DayOfMonth, IDaily Then), T> selector1, 
            Func<(DayInMonth DayInMonth, DayOfWeek DayOfWeek, IDaily Then), T> selector2) 
            => monthly
            .GetThe()
            .Select(daily => daily.DayOfMonth != 0 
                ? selector1((daily.DayOfMonth, daily)) 
                : selector2((daily.DayInMonth, daily.DayOfWeek, daily)));
        /// <summary>
        /// Maps every day of the month occurrence of a monthly recurrence to the selected form.
        /// </summary>
        /// <typeparam name="T">Any type.</typeparam>
        /// <param name="monthly">Monthly recurrence</param>
        /// <param name="selector">Mapping function.</param>
        /// <returns>Every day of the month occurrence projected by the mapping function.</returns>
        public static IEnumerable<T> SelectTheDay<T>(this IMonthly monthly, Func<(int DayOfMonth, IDaily Then), T> selector)
            => monthly.GetTheDay().Select(daily => selector((daily.DayOfMonth, daily)));
        /// <summary>
        /// Maps every day of the week in the month occurrence of a monthly recurrence to the selected form.
        /// </summary>
        /// <typeparam name="T">Any type.</typeparam>
        /// <param name="monthly">Monthly recurrence</param>
        /// <param name="selector">Maps the day in the month.</param>
        /// <returns>Every day of the week occurrence projected by the mapping function.</returns>
        public static IEnumerable<T> SelectTheWeekDay<T>(this IMonthly monthly, Func<(DayInMonth DayInMonth, DayOfWeek DayOfWeek, IDaily Then), T> selector)
            => monthly.GetTheWeekDay().Select(daily => selector((daily.DayInMonth, daily.DayOfWeek, daily)));
        /// <summary>
        /// Maps the month occurrences of a yearly recurrence to the selected form.
        /// </summary>
        /// <typeparam name="T">Any type.</typeparam>
        /// <param name="yearly">Yearly recurrence.</param>
        /// <param name="selector">Mapping function.</param>
        /// <returns>Every month occurrence projected by the mapping function.</returns>
        public static IEnumerable<T> SelectIn<T>(this IYearly yearly, Func<(int Month, IMonthly Then), T> selector)
            => yearly.GetIn().Select(x => selector((x.Month, x)));
    }
}