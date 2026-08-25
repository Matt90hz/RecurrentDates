using IncaTechnologies.Recurrence;

namespace IncaTechnologies.Recurrence.Radzen;

/// <summary>
/// Provides extension methods for value-based equality comparison of recurrence objects.
/// </summary>
public static class RecurrenceEquality
{
    extension(IRecurrent recurrent)
    {
        /// <summary>
        /// Determines whether this recurrence has the same configured values as another recurrence.
        /// </summary>
        /// <param name="recurrent1">The recurrence to compare with this recurrence.</param>
        /// <returns><see langword="true"/> when both recurrences have the same values; otherwise, <see langword="false"/>.</returns>
        public bool ValueEquals(IRecurrent? recurrent1)
            => recurrent is not null
            && recurrent1 is not null
            && recurrent.ToJson() == recurrent1.ToJson();
    }

    extension(IDaily daily)
    {
        /// <summary>
        /// Determines whether the daily part of this recurrence has the same configured times as another daily recurrence.
        /// </summary>
        /// <param name="daily1">The daily recurrence to compare with the daily part of this recurrence.</param>
        /// <returns><see langword="true"/> when both daily recurrences have the same times; otherwise, <see langword="false"/>.</returns>
        public bool ValueEquals(IDaily? daily1)
            => daily1 is not null
            && daily1.SelectAt(x => x).SequenceEqual(daily.SelectAt(x => x));
    }

    extension(IWeekly weekly)
    {
        /// <summary>
        /// Determines whether the weekly part of this recurrence has the same configured days and times as another weekly recurrence.
        /// </summary>
        /// <param name="weekly1">The weekly recurrence to compare with the weekly part of this recurrence.</param>
        /// <returns><see langword="true"/> when both weekly recurrences have the same days and times; otherwise, <see langword="false"/>.</returns>
        public bool ValueEquals(IWeekly? weekly1)
            => weekly1 is not null
            && SequencesEqual(
                weekly.SelectOn(x => x),
                weekly1.SelectOn(x => x),
                (x, y) => x.DayOfWeek == y.DayOfWeek && x.Then.ValueEquals(y.Then));
    }

    extension(IMonthly monthly)
    {
        /// <summary>
        /// Determines whether the monthly part of this recurrence has the same configured days and times as another monthly recurrence.
        /// </summary>
        /// <param name="monthly1">The monthly recurrence to compare with the monthly part of this recurrence.</param>
        /// <returns><see langword="true"/> when both monthly recurrences have the same days and times; otherwise, <see langword="false"/>.</returns>
        public bool ValueEquals(IMonthly? monthly1)
            => monthly1 is not null
            && SequencesEqual(
                monthly.SelectTheDay(x => (x.DayOfMonth, x.Then)),
                monthly1.SelectTheDay(x => (x.DayOfMonth, x.Then)),
                (x, y) => x.DayOfMonth == y.DayOfMonth && x.Then.ValueEquals(y.Then))
            && SequencesEqual(
                monthly.SelectTheWeekDay(x => (x.DayInMonth, x.DayOfWeek, x.Then)),
                monthly1.SelectTheWeekDay(x => (x.DayInMonth, x.DayOfWeek, x.Then)),
                (x, y) => x.DayInMonth == y.DayInMonth
                    && x.DayOfWeek == y.DayOfWeek
                    && x.Then.ValueEquals(y.Then));
    }

    extension(IYearly yearly)
    {
        /// <summary>
        /// Determines whether the yearly part of this recurrence has the same configured months, days, and times as another yearly recurrence.
        /// </summary>
        /// <param name="yearly1">The yearly recurrence to compare with the yearly part of this recurrence.</param>
        /// <returns><see langword="true"/> when both yearly recurrences have the same months, days, and times; otherwise, <see langword="false"/>.</returns>
        public bool ValueEquals(IYearly? yearly1)
            => yearly1 is not null
            && SequencesEqual(
                yearly.SelectIn(x => x),
                yearly1.SelectIn(x => x),
                (x, y) => x.Month == y.Month && x.Then.ValueEquals(y.Then));
    }

    private static bool SequencesEqual<T>(IEnumerable<T> first, IEnumerable<T> second, Func<T, T, bool> comparer)
    {
        var firstValues = first.ToArray();
        var secondValues = second.ToArray();

        return firstValues.Length == secondValues.Length
            && firstValues.Zip(secondValues).All(x => comparer(x.First, x.Second));
    }
}