using System;
using System.Collections.Generic;

namespace IncaTechnologies.Recurrence
{
    /// <summary>
    /// Base interface for recurrences.
    /// </summary>
    public interface IRecurrent { }
    /// <summary>
    /// Represents an yearly recurrence, is top level recurrence.
    /// Can be created using <see cref="Occurs.EveryYear"/>.
    /// The monthly occurrences can be configured using extensions form <see cref="Occurs"/>.
    /// </summary>
    public interface IYearly : IRecurrent { }
    /// <summary>
    /// Represents a monthly recurrence.
    /// Can be created using <see cref="Occurs.EveryMonth"/>.
    /// The weekly and daily occurrences can be configured using extensions form <see cref="Occurs"/>.
    /// </summary>
    public interface IMonthly : IRecurrent { }
    /// <summary>
    /// Represents a weekly recurrence.
    /// Can be created using <see cref="Occurs.EveryWeek"/>.
    /// The daily occurrences can be configured using extensions form <see cref="Occurs"/>.
    /// </summary>
    public interface IWeekly : IRecurrent { }
    /// <summary>
    /// Represents a daily recurrence.
    /// Can be created using <see cref="Occurs.EveryDay"/>.
    /// This is a base the base level recurrence.
    /// The hourly occurrences can be configured using extensions form <see cref="Occurs"/>.
    /// </summary>
    public interface IDaily : IRecurrent { }
    /// <summary>
    /// Represents a hourly recurrence.
    /// The minutely occurrence can be configured using extensions form <see cref="Occurs"/>.
    /// </summary>
    public interface IHourly : IRecurrent { }
    /// <summary>
    /// Represents a minutely recurrence.
    /// The secondly occurrence can be configured using extensions form <see cref="Occurs"/>.
    /// </summary>
    public interface IMinutely : IRecurrent { }
    /// <summary>
    /// Represents a secondly recurrence.
    /// The occurrences cannot be configured any further.
    /// </summary>
    public interface ISecondly : IRecurrent { }
    internal sealed class Yearly : IYearly
    {
        public List<Monthly> In { get; } = new List<Monthly>();
    }
    internal sealed class Monthly : IMonthly
    {
        public Yearly Yearly { get; set; }
        public int Month { get; set; }
        public List<Daily> TheDay { get; } = new List<Daily>();
        public List<Daily> InDay { get; } = new List<Daily>();
    }
    internal sealed class Weekly : IWeekly
    {
        public Monthly Monthly { get; set; }
        public List<Daily> On { get; } = new List<Daily>();
    }
    internal sealed class Daily : IDaily
    {
        public Weekly Weekly { get; set; }
        public Monthly Monthly { get; set; }
        public int DayOfMonth { get; set; }
        public DayInMonth DayInMonth { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public List<Hourly> At { get; } = new List<Hourly>();
    }
    internal sealed class Hourly : IHourly
    {
        public int Hour { get; set; }
        public Daily Daily { get; set; }
        public Minutely Minutely { get; set; }
    }
    internal sealed class Minutely : IMinutely
    {
        public Hourly Hourly { get; set; }
        public int Minute { get; set; }
        public Secondly Secondly { get; set; }
    }
    internal sealed class Secondly : ISecondly
    {
        public Minutely Minutely { get; set; }
        public int Second { get; set; }
    }
    /// <summary>
    /// Indicates the place in the month of a week day.
    /// The tuple <see cref="DayInMonth"/> and <see cref="DayOfWeek"/> are used to identify the occurrence.
    /// For example (<see cref="DayInMonth.First"/>, <see cref="DayOfWeek.Sunday"/>).
    /// </summary>
    public enum DayInMonth { First, Second, Third, Fourth, Last }
    /// <summary>
    /// Factories and builders for <see cref="IRecurrent"/> and derived.
    /// </summary>
    public static class Occurs
    {
        #region Yearly
        /// <summary>
        /// Creates a yearly recurrence.
        /// </summary>
        /// <remarks>
        /// By default an yearly recurrence take place every January the 1st, of every year at midnight.
        /// </remarks>
        /// <returns>
        /// <see cref="IMonthly"/> to fluently configure the monthly occurrence.
        /// Use <see cref="In"/> to configure more than one monthly occurrence.
        /// </returns>
        public static IMonthly EveryYear() => new Monthly()
        {
            Yearly = new Yearly()
        };
        /// <summary>
        /// Lets you configure more than one monthly occurrence for a yearly recurrence.
        /// <example>
        /// <code>
        /// Occurs.EveryYear().In(x => 
        /// {
        ///     x.May();
        ///     x.April().The(x =>
        ///     {
        ///         x.Day(15);
        ///         x.FirstMonday();
        ///         x.SecondFriday().At(x =>
        ///         {
        ///             x.Hour(9).Minute(30);
        ///             x.Hour(12).Minute(45).Second(20);
        ///         });
        ///     });
        /// });            
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="recurrence">Monthly occurrence to be configured as part of an yearly recurrence.</param>
        /// <param name="builder">Monthly configuration builder.</param>
        /// <returns><see cref="IRecurrent"/> that can be enumerated or serialized</returns>
        public static IRecurrent In(this IMonthly recurrence, Action<IMonthly> builder)
        {
            builder(recurrence);
            return recurrence;
        }
        /// <summary>
        /// Configures te monthly occurrence of a yearly recurrence to happen in January.
        /// </summary>
        /// <remarks>
        /// By default it will occur the 1st at midnight.
        /// </remarks>
        /// <param name="recurrence">Monthly occurrence to be configured as part of an yearly recurrence.</param>
        /// <returns><see cref="IWeekly"/> to fluently configure the weekly occurrence of a monthly recurrence.</returns>
        public static IWeekly January(this IMonthly recurrence) => recurrence.Month(1);
        /// <summary>
        /// Configures te monthly occurrence of a yearly recurrence to happen in January.
        /// </summary>
        /// <remarks>
        /// By default it will occur the 1st at midnight.
        /// </remarks>
        /// <param name="recurrence">Monthly occurrence to be configured as part of an yearly recurrence.</param>
        /// <returns><see cref="IWeekly"/> to fluently configure the weekly occurrence of a monthly recurrence.</returns>
        public static IWeekly February(this IMonthly recurrence) => recurrence.Month(2);
        /// <summary>
        /// Configures te monthly occurrence of a yearly recurrence to happen in February.
        /// </summary>
        /// <remarks>
        /// By default it will occur the 1st at midnight.
        /// </remarks>
        /// <param name="recurrence">Monthly occurrence to be configured as part of an yearly recurrence.</param>
        /// <returns><see cref="IWeekly"/> to fluently configure the weekly occurrence of a monthly recurrence.</returns>
        public static IWeekly March(this IMonthly recurrence) => recurrence.Month(3);
        /// <summary>
        /// Configures te monthly occurrence of a yearly recurrence to happen in March.
        /// </summary>
        /// <remarks>
        /// By default it will occur the 1st at midnight.
        /// </remarks>
        /// <param name="recurrence">Monthly occurrence to be configured as part of an yearly recurrence.</param>
        /// <returns><see cref="IWeekly"/> to fluently configure the weekly occurrence of a monthly recurrence.</returns>
        public static IWeekly April(this IMonthly recurrence) => recurrence.Month(4);
        /// <summary>
        /// Configures te monthly occurrence of a yearly recurrence to happen in April.
        /// </summary>
        /// <remarks>
        /// By default it will occur the 1st at midnight.
        /// </remarks>
        /// <param name="recurrence">Monthly occurrence to be configured as part of an yearly recurrence.</param>
        /// <returns><see cref="IWeekly"/> to fluently configure the weekly occurrence of a monthly recurrence.</returns>
        public static IWeekly May(this IMonthly recurrence) => recurrence.Month(5);
        /// <summary>
        /// Configures te monthly occurrence of a yearly recurrence to happen in May.
        /// </summary>
        /// <remarks>
        /// By default it will occur the 1st at midnight.
        /// </remarks>
        /// <param name="recurrence">Monthly occurrence to be configured as part of an yearly recurrence.</param>
        /// <returns><see cref="IWeekly"/> to fluently configure the weekly occurrence of a monthly recurrence.</returns>
        public static IWeekly June(this IMonthly recurrence) => recurrence.Month(6);
        /// <summary>
        /// Configures te monthly occurrence of a yearly recurrence to happen in June.
        /// </summary>
        /// <remarks>
        /// By default it will occur the 1st at midnight.
        /// </remarks>
        /// <param name="recurrence">Monthly occurrence to be configured as part of an yearly recurrence.</param>
        /// <returns><see cref="IWeekly"/> to fluently configure the weekly occurrence of a monthly recurrence.</returns>
        public static IWeekly July(this IMonthly recurrence) => recurrence.Month(7);
        /// <summary>
        /// Configures te monthly occurrence of a yearly recurrence to happen in July.
        /// </summary>
        /// <remarks>
        /// By default it will occur the 1st at midnight.
        /// </remarks>
        /// <param name="recurrence">Monthly occurrence to be configured as part of an yearly recurrence.</param>
        /// <returns><see cref="IWeekly"/> to fluently configure the weekly occurrence of a monthly recurrence.</returns>
        public static IWeekly August(this IMonthly recurrence) => recurrence.Month(8);
        /// <summary>
        /// Configures te monthly occurrence of a yearly recurrence to happen in August.
        /// </summary>
        /// <remarks>
        /// By default it will occur the 1st at midnight.
        /// </remarks>
        /// <param name="recurrence">Monthly occurrence to be configured as part of an yearly recurrence.</param>
        /// <returns><see cref="IWeekly"/> to fluently configure the weekly occurrence of a monthly recurrence.</returns>
        public static IWeekly September(this IMonthly recurrence) => recurrence.Month(9);
        /// <summary>
        /// Configures te monthly occurrence of a yearly recurrence to happen in September.
        /// </summary>
        /// <remarks>
        /// By default it will occur the 1st at midnight.
        /// </remarks>
        /// <param name="recurrence">Monthly occurrence to be configured as part of an yearly recurrence.</param>
        /// <returns><see cref="IWeekly"/> to fluently configure the weekly occurrence of a monthly recurrence.</returns>
        public static IWeekly October(this IMonthly recurrence) => recurrence.Month(10);
        /// <summary>
        /// Configures te monthly occurrence of a yearly recurrence to happen in October.
        /// </summary>
        /// <remarks>
        /// By default it will occur the 1st at midnight.
        /// </remarks>
        /// <param name="recurrence">Monthly occurrence to be configured as part of an yearly recurrence.</param>
        /// <returns><see cref="IWeekly"/> to fluently configure the weekly occurrence of a monthly recurrence.</returns>
        public static IWeekly November(this IMonthly recurrence) => recurrence.Month(11);
        /// <summary>
        /// Configures te monthly occurrence of a yearly recurrence to happen in November.
        /// </summary>
        /// <remarks>
        /// By default it will occur the 1st at midnight.
        /// </remarks>
        /// <param name="recurrence">Monthly occurrence to be configured as part of an yearly recurrence.</param>
        /// <returns><see cref="IWeekly"/> to fluently configure the weekly occurrence of a monthly recurrence.</returns>
        public static IWeekly December(this IMonthly recurrence) => recurrence.Month(12);
        internal static IWeekly Month(this IMonthly recurrence, int month)
        {
            if(month<1 ||month > 12)
            {
                throw new ArgumentOutOfRangeException(nameof(month), month, "The month must be between 1 and 12.");
            }

            var weekly = new Weekly();
            if (recurrence is Monthly monthly)
            {
                var monthlyClone = new Monthly
                {
                    Yearly = monthly.Yearly,
                    Month = month,
                };
                weekly.Monthly = monthlyClone;
                monthlyClone.Yearly.In.Add(monthlyClone);
            }

            return weekly;
        }
        #endregion Yearly
        #region Monthly
        /// <summary>
        /// Creates a monthly recurrence.
        /// </summary>
        /// <remarks>
        /// By default a monthly recurrence take place every 1st, of every month at midnight.
        /// </remarks>
        /// <returns>
        /// <see cref="IWeekly"/> to fluently configure the weekly or daily occurrence.
        /// Use <see cref="The"/> to configure more than one weekly or daily occurrence.
        /// </returns>
        public static IWeekly EveryMonth() => new Weekly
        {
            Monthly = new Monthly()
        };
        /// <summary>
        /// Lets you configure more than one weekly occurrence for a monthly recurrence.
        /// <example>
        /// <code>
        /// Occurs.EveryMonth().The(x =>
        /// {
        ///     x.Day(1);
        ///     x.Day(2).Hour(12).Minute(30);
        ///     x.SecondFriday();
        /// });
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="recurrence">Weekly occurrence to be configured as part of a monthly recurrence.</param>
        /// <param name="builder">Weekly configuration builder.</param>
        /// <returns><see cref="IRecurrent"/> that can be enumerated or serialized</returns>
        public static IRecurrent The(this IWeekly recurrence, Action<IWeekly> builder)
        {
            builder(recurrence);
            return recurrence;
        }
        /// <summary>
        /// Set a monthly recurrence to occur in a given day of the month.
        /// </summary>
        /// <remarks>
        /// Only if the day is present in the month will be actually enumerated (see <see cref="Enumerator.AsEnumerable(IRecurrent, DateTime, DateTime)"/>).
        /// For example the 31st will not occur in February.
        /// </remarks>
        /// <param name="recurrence">The weekly occurrence of a monthly recurrence to be configured.</param>
        /// <param name="day">The day of the month to be configured.</param>
        /// <returns><see cref="IHourly"/> to fluently configure the daily occurrence.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The <paramref name="day"/> can range form 1 to 31.</exception>
        public static IHourly Day(this IWeekly recurrence, int day)
        {
            if (day < 1 || day > 31)
            {
                throw new ArgumentOutOfRangeException(nameof(day), day, "The month must be between 1 and 31.");
            }

            var hourly = new Hourly();
            if (recurrence is Weekly weekly)
            {
                var daily = new Daily
                {
                    DayOfMonth = day,
                    Monthly = weekly.Monthly,
                };
                hourly.Daily = daily;
                daily.Monthly.TheDay.Add(daily);
            }

            return hourly;
        }
        /// <summary>
        /// Set a monthly recurrence to occur the first Sunday of the month.
        /// </summary>
        /// <remarks>
        /// By default the recurrence will occur the selected day at midnight.
        /// </remarks>
        /// <param name="recurrence">The weekly occurrence of a monthly recurrence to be configured.</param>
        /// <returns><see cref="IHourly"/> to fluently configure the daily occurrence.</returns>
        public static IHourly FirstSunday(this IWeekly recurrence) => recurrence.First(DayOfWeek.Sunday);
        /// <summary>
        /// Set a monthly recurrence to occur the first Sunday of the month.
        /// </summary>
        /// <remarks>
        /// By default the recurrence will occur the selected day at midnight.
        /// </remarks>
        /// <param name="recurrence">The weekly occurrence of a monthly recurrence to be configured.</param>
        /// <returns><see cref="IHourly"/> to fluently configure the daily occurrence.</returns>
        public static IHourly FirstMonday(this IWeekly recurrence) => recurrence.First(DayOfWeek.Monday);
        /// <summary>
        /// Set a monthly recurrence to occur the first Monday of the month.
        /// </summary>
        /// <remarks>
        /// By default the recurrence will occur the selected day at midnight.
        /// </remarks>
        /// <param name="recurrence">The weekly occurrence of a monthly recurrence to be configured.</param>
        /// <returns><see cref="IHourly"/> to fluently configure the daily occurrence.</returns>
        public static IHourly FirstTuesday(this IWeekly recurrence) => recurrence.First(DayOfWeek.Tuesday);
        /// <summary>
        /// Set a monthly recurrence to occur the first Tuesday of the month.
        /// </summary>
        /// <remarks>
        /// By default the recurrence will occur the selected day at midnight.
        /// </remarks>
        /// <param name="recurrence">The weekly occurrence of a monthly recurrence to be configured.</param>
        /// <returns><see cref="IHourly"/> to fluently configure the daily occurrence.</returns>
        public static IHourly FirstWednesday(this IWeekly recurrence) => recurrence.First(DayOfWeek.Wednesday);
        /// <summary>
        /// Set a monthly recurrence to occur the first Wednesday of the month.
        /// </summary>
        /// <remarks>
        /// By default the recurrence will occur the selected day at midnight.
        /// </remarks>
        /// <param name="recurrence">The weekly occurrence of a monthly recurrence to be configured.</param>
        /// <returns><see cref="IHourly"/> to fluently configure the daily occurrence.</returns>
        public static IHourly FirstThursday(this IWeekly recurrence) => recurrence.First(DayOfWeek.Thursday);
        /// <summary>
        /// Set a monthly recurrence to occur the first Thursday of the month.
        /// </summary>
        /// <remarks>
        /// By default the recurrence will occur the selected day at midnight.
        /// </remarks>
        /// <param name="recurrence">The weekly occurrence of a monthly recurrence to be configured.</param>
        /// <returns><see cref="IHourly"/> to fluently configure the daily occurrence.</returns>
        public static IHourly FirstFriday(this IWeekly recurrence) => recurrence.First(DayOfWeek.Friday);
        /// <summary>
        /// Set a monthly recurrence to occur the first Friday of the month.
        /// </summary>
        /// <remarks>
        /// By default the recurrence will occur the selected day at midnight.
        /// </remarks>
        /// <param name="recurrence">The weekly occurrence of a monthly recurrence to be configured.</param>
        /// <returns><see cref="IHourly"/> to fluently configure the daily occurrence.</returns>
        public static IHourly FirstSaturday(this IWeekly recurrence) => recurrence.First(DayOfWeek.Saturday);
        /// <summary>
        /// Set a monthly recurrence to occur the first Saturday of the month.
        /// </summary>
        /// <remarks>
        /// By default the recurrence will occur the selected day at midnight.
        /// </remarks>
        /// <param name="recurrence">The weekly occurrence of a monthly recurrence to be configured.</param>
        /// <returns><see cref="IHourly"/> to fluently configure the daily occurrence.</returns>
        public static IHourly SecondSunday(this IWeekly recurrence) => recurrence.Second(DayOfWeek.Sunday);
        /// <summary>
        /// Set a monthly recurrence to occur the second Sunday of the month.
        /// </summary>
        /// <remarks>
        /// By default the recurrence will occur the selected day at midnight.
        /// </remarks>
        /// <param name="recurrence">The weekly occurrence of a monthly recurrence to be configured.</param>
        /// <returns><see cref="IHourly"/> to fluently configure the daily occurrence.</returns>
        public static IHourly SecondMonday(this IWeekly recurrence) => recurrence.Second(DayOfWeek.Monday);
        /// <summary>
        /// Set a monthly recurrence to occur the second Monday of the month.
        /// </summary>
        /// <remarks>
        /// By default the recurrence will occur the selected day at midnight.
        /// </remarks>
        /// <param name="recurrence">The weekly occurrence of a monthly recurrence to be configured.</param>
        /// <returns><see cref="IHourly"/> to fluently configure the daily occurrence.</returns>
        public static IHourly SecondTuesday(this IWeekly recurrence) => recurrence.Second(DayOfWeek.Tuesday);
        /// <summary>
        /// Set a monthly recurrence to occur the second Tuesday of the month.
        /// </summary>
        /// <remarks>
        /// By default the recurrence will occur the selected day at midnight.
        /// </remarks>
        /// <param name="recurrence">The weekly occurrence of a monthly recurrence to be configured.</param>
        /// <returns><see cref="IHourly"/> to fluently configure the daily occurrence.</returns>
        public static IHourly SecondWednesday(this IWeekly recurrence) => recurrence.Second(DayOfWeek.Wednesday);
        /// <summary>
        /// Set a monthly recurrence to occur the second Wednesday of the month.
        /// </summary>
        /// <remarks>
        /// By default the recurrence will occur the selected day at midnight.
        /// </remarks>
        /// <param name="recurrence">The weekly occurrence of a monthly recurrence to be configured.</param>
        /// <returns><see cref="IHourly"/> to fluently configure the daily occurrence.</returns>
        public static IHourly SecondThursday(this IWeekly recurrence) => recurrence.Second(DayOfWeek.Thursday);
        /// <summary>
        /// Set a monthly recurrence to occur the second Thursday of the month.
        /// </summary>
        /// <remarks>
        /// By default the recurrence will occur the selected day at midnight.
        /// </remarks>
        /// <param name="recurrence">The weekly occurrence of a monthly recurrence to be configured.</param>
        /// <returns><see cref="IHourly"/> to fluently configure the daily occurrence.</returns>
        public static IHourly SecondFriday(this IWeekly recurrence) => recurrence.Second(DayOfWeek.Friday);
        /// <summary>
        /// Set a monthly recurrence to occur the second Friday of the month.
        /// </summary>
        /// <remarks>
        /// By default the recurrence will occur the selected day at midnight.
        /// </remarks>
        /// <param name="recurrence">The weekly occurrence of a monthly recurrence to be configured.</param>
        /// <returns><see cref="IHourly"/> to fluently configure the daily occurrence.</returns>
        public static IHourly SecondSaturday(this IWeekly recurrence) => recurrence.Second(DayOfWeek.Saturday);
        /// <summary>
        /// Set a monthly recurrence to occur the second Saturday of the month.
        /// </summary>
        /// <remarks>
        /// By default the recurrence will occur the selected day at midnight.
        /// </remarks>
        /// <param name="recurrence">The weekly occurrence of a monthly recurrence to be configured.</param>
        /// <returns><see cref="IHourly"/> to fluently configure the daily occurrence.</returns>
        public static IHourly ThirdSunday(this IWeekly recurrence) => recurrence.Third(DayOfWeek.Sunday);
        /// <summary>
        /// Set a monthly recurrence to occur the third Sunday of the month.
        /// </summary>
        /// <remarks>
        /// By default the recurrence will occur the selected day at midnight.
        /// </remarks>
        /// <param name="recurrence">The weekly occurrence of a monthly recurrence to be configured.</param>
        /// <returns><see cref="IHourly"/> to fluently configure the daily occurrence.</returns>
        public static IHourly ThirdMonday(this IWeekly recurrence) => recurrence.Third(DayOfWeek.Monday);
        /// <summary>
        /// Set a monthly recurrence to occur the third Monday of the month.
        /// </summary>
        /// <remarks>
        /// By default the recurrence will occur the selected day at midnight.
        /// </remarks>
        /// <param name="recurrence">The weekly occurrence of a monthly recurrence to be configured.</param>
        /// <returns><see cref="IHourly"/> to fluently configure the daily occurrence.</returns>
        public static IHourly ThirdTuesday(this IWeekly recurrence) => recurrence.Third(DayOfWeek.Tuesday);
        /// <summary>
        /// Set a monthly recurrence to occur the third Tuesday of the month.
        /// </summary>
        /// <remarks>
        /// By default the recurrence will occur the selected day at midnight.
        /// </remarks>
        /// <param name="recurrence">The weekly occurrence of a monthly recurrence to be configured.</param>
        /// <returns><see cref="IHourly"/> to fluently configure the daily occurrence.</returns>
        public static IHourly ThirdWednesday(this IWeekly recurrence) => recurrence.Third(DayOfWeek.Wednesday);
        /// <summary>
        /// Set a monthly recurrence to occur the third Wednesday of the month.
        /// </summary>
        /// <remarks>
        /// By default the recurrence will occur the selected day at midnight.
        /// </remarks>
        /// <param name="recurrence">The weekly occurrence of a monthly recurrence to be configured.</param>
        /// <returns><see cref="IHourly"/> to fluently configure the daily occurrence.</returns>
        public static IHourly ThirdThursday(this IWeekly recurrence) => recurrence.Third(DayOfWeek.Thursday);
        /// <summary>
        /// Set a monthly recurrence to occur the third Thursday of the month.
        /// </summary>
        /// <remarks>
        /// By default the recurrence will occur the selected day at midnight.
        /// </remarks>
        /// <param name="recurrence">The weekly occurrence of a monthly recurrence to be configured.</param>
        /// <returns><see cref="IHourly"/> to fluently configure the daily occurrence.</returns>
        public static IHourly ThirdFriday(this IWeekly recurrence) => recurrence.Third(DayOfWeek.Friday);
        /// <summary>
        /// Set a monthly recurrence to occur the third Friday of the month.
        /// </summary>
        /// <remarks>
        /// By default the recurrence will occur the selected day at midnight.
        /// </remarks>
        /// <param name="recurrence">The weekly occurrence of a monthly recurrence to be configured.</param>
        /// <returns><see cref="IHourly"/> to fluently configure the daily occurrence.</returns>
        public static IHourly ThirdSaturday(this IWeekly recurrence) => recurrence.Third(DayOfWeek.Saturday);
        /// <summary>
        /// Set a monthly recurrence to occur the third Saturday of the month.
        /// </summary>
        /// <remarks>
        /// By default the recurrence will occur the selected day at midnight.
        /// The forth may also be the last day of the month. 
        /// For example January, the 25th is the fourth and last Tuesday of the month.
        /// When enumerated the 25th will appear twice if the monthly recurrence is configured to occur the fourth and the last day of the moth.
        /// </remarks>
        /// <param name="recurrence">The weekly occurrence of a monthly recurrence to be configured.</param>
        /// <returns><see cref="IHourly"/> to fluently configure the daily occurrence.</returns>
        public static IHourly FourthSunday(this IWeekly recurrence) => recurrence.Fourth(DayOfWeek.Sunday);
        /// <summary>
        /// Set a monthly recurrence to occur the fourth Sunday of the month.
        /// </summary>
        /// <remarks>
        /// By default the recurrence will occur the selected day at midnight.
        /// The forth may also be the last day of the month. 
        /// For example January, the 25th is the fourth and last Tuesday of the month.
        /// When enumerated the 25th will appear twice if the monthly recurrence is configured to occur the fourth and the last day of the moth.
        /// </remarks>
        /// <param name="recurrence">The weekly occurrence of a monthly recurrence to be configured.</param>
        /// <returns><see cref="IHourly"/> to fluently configure the daily occurrence.</returns>
        public static IHourly FourthMonday(this IWeekly recurrence) => recurrence.Fourth(DayOfWeek.Monday);
        /// <summary>
        /// Set a monthly recurrence to occur the fourth Monday of the month.
        /// </summary>
        /// <remarks>
        /// By default the recurrence will occur the selected day at midnight.
        /// The forth may also be the last day of the month. 
        /// For example January, the 25th is the fourth and last Tuesday of the month.
        /// When enumerated the 25th will appear twice if the monthly recurrence is configured to occur the fourth and the last day of the moth.
        /// </remarks>
        /// <param name="recurrence">The weekly occurrence of a monthly recurrence to be configured.</param>
        /// <returns><see cref="IHourly"/> to fluently configure the daily occurrence.</returns>
        public static IHourly FourthTuesday(this IWeekly recurrence) => recurrence.Fourth(DayOfWeek.Tuesday);
        /// <summary>
        /// Set a monthly recurrence to occur the fourth Tuesday of the month.
        /// </summary>
        /// <remarks>
        /// By default the recurrence will occur the selected day at midnight.
        /// The forth may also be the last day of the month. 
        /// For example January, the 25th is the fourth and last Tuesday of the month.
        /// When enumerated the 25th will appear twice if the monthly recurrence is configured to occur the fourth and the last day of the moth.
        /// </remarks>
        /// <param name="recurrence">The weekly occurrence of a monthly recurrence to be configured.</param>
        /// <returns><see cref="IHourly"/> to fluently configure the daily occurrence.</returns>
        public static IHourly FourthWednesday(this IWeekly recurrence) => recurrence.Fourth(DayOfWeek.Wednesday);
        /// <summary>
        /// Set a monthly recurrence to occur the fourth Wednesday of the month.
        /// </summary>
        /// <remarks>
        /// By default the recurrence will occur the selected day at midnight.
        /// The forth may also be the last day of the month. 
        /// For example January, the 25th is the fourth and last Tuesday of the month.
        /// When enumerated the 25th will appear twice if the monthly recurrence is configured to occur the fourth and the last day of the moth.
        /// </remarks>
        /// <param name="recurrence">The weekly occurrence of a monthly recurrence to be configured.</param>
        /// <returns><see cref="IHourly"/> to fluently configure the daily occurrence.</returns>
        public static IHourly FourthThursday(this IWeekly recurrence) => recurrence.Fourth(DayOfWeek.Thursday);
        /// <summary>
        /// Set a monthly recurrence to occur the fourth Thursday of the month.
        /// </summary>
        /// <remarks>
        /// By default the recurrence will occur the selected day at midnight.
        /// The forth may also be the last day of the month. 
        /// For example January, the 25th is the fourth and last Tuesday of the month.
        /// When enumerated the 25th will appear twice if the monthly recurrence is configured to occur the fourth and the last day of the moth.
        /// </remarks>
        /// <param name="recurrence">The weekly occurrence of a monthly recurrence to be configured.</param>
        /// <returns><see cref="IHourly"/> to fluently configure the daily occurrence.</returns>
        public static IHourly FourthFriday(this IWeekly recurrence) => recurrence.Fourth(DayOfWeek.Friday);
        /// <summary>
        /// Set a monthly recurrence to occur the fourth Friday of the month.
        /// </summary>
        /// <remarks>
        /// By default the recurrence will occur the selected day at midnight.
        /// The forth may also be the last day of the month. 
        /// For example January, the 25th is the fourth and last Tuesday of the month.
        /// When enumerated the 25th will appear twice if the monthly recurrence is configured to occur the fourth and the last day of the moth.
        /// </remarks>
        /// <param name="recurrence">The weekly occurrence of a monthly recurrence to be configured.</param>
        /// <returns><see cref="IHourly"/> to fluently configure the daily occurrence.</returns>
        public static IHourly FourthSaturday(this IWeekly recurrence) => recurrence.Fourth(DayOfWeek.Saturday);
        /// <summary>
        /// Set a monthly recurrence to occur the fourth Saturday of the month.
        /// </summary>
        /// <remarks>
        /// By default the recurrence will occur the selected day at midnight.
        /// The forth may also be the last day of the month. 
        /// For example January, the 25th is the fourth and last Tuesday of the month.
        /// When enumerated the 25th will appear twice if the monthly recurrence is configured to occur the fourth and the last day of the moth.
        /// </remarks>
        /// <param name="recurrence">The weekly occurrence of a monthly recurrence to be configured.</param>
        /// <returns><see cref="IHourly"/> to fluently configure the daily occurrence.</returns>
        public static IHourly LastSunday(this IWeekly recurrence) => recurrence.Last(DayOfWeek.Sunday);
        /// <summary>
        /// Set a monthly recurrence to occur the last Sunday of the month.
        /// </summary>
        /// <remarks>
        /// By default the recurrence will occur the selected day at midnight.
        /// The forth may also be the last day of the month. 
        /// For example January, the 25th is the fourth and last Tuesday of the month.
        /// When enumerated the 25th will appear twice if the monthly recurrence is configured to occur the fourth and the last day of the moth.
        /// </remarks>
        /// <param name="recurrence">The weekly occurrence of a monthly recurrence to be configured.</param>
        /// <returns><see cref="IHourly"/> to fluently configure the daily occurrence.</returns>
        public static IHourly LastMonday(this IWeekly recurrence) => recurrence.Last(DayOfWeek.Monday);
        /// <summary>
        /// Set a monthly recurrence to occur the last Monday of the month.
        /// </summary>
        /// <remarks>
        /// By default the recurrence will occur the selected day at midnight.
        /// The forth may also be the last day of the month. 
        /// For example January, the 25th is the fourth and last Tuesday of the month.
        /// When enumerated the 25th will appear twice if the monthly recurrence is configured to occur the fourth and the last day of the moth.
        /// </remarks>
        /// <param name="recurrence">The weekly occurrence of a monthly recurrence to be configured.</param>
        /// <returns><see cref="IHourly"/> to fluently configure the daily occurrence.</returns>
        public static IHourly LastTuesday(this IWeekly recurrence) => recurrence.Last(DayOfWeek.Tuesday);
        /// <summary>
        /// Set a monthly recurrence to occur the last Tuesday of the month.
        /// </summary>
        /// <remarks>
        /// By default the recurrence will occur the selected day at midnight.
        /// The forth may also be the last day of the month. 
        /// For example January, the 25th is the fourth and last Tuesday of the month.
        /// When enumerated the 25th will appear twice if the monthly recurrence is configured to occur the fourth and the last day of the moth.
        /// </remarks>
        /// <param name="recurrence">The weekly occurrence of a monthly recurrence to be configured.</param>
        /// <returns><see cref="IHourly"/> to fluently configure the daily occurrence.</returns>
        public static IHourly LastWednesday(this IWeekly recurrence) => recurrence.Last(DayOfWeek.Wednesday);
        /// <summary>
        /// Set a monthly recurrence to occur the last Wednesday of the month.
        /// </summary>
        /// <remarks>
        /// By default the recurrence will occur the selected day at midnight.
        /// The forth may also be the last day of the month. 
        /// For example January, the 25th is the fourth and last Tuesday of the month.
        /// When enumerated the 25th will appear twice if the monthly recurrence is configured to occur the fourth and the last day of the moth.
        /// </remarks>
        /// <param name="recurrence">The weekly occurrence of a monthly recurrence to be configured.</param>
        /// <returns><see cref="IHourly"/> to fluently configure the daily occurrence.</returns>
        public static IHourly LastThursday(this IWeekly recurrence) => recurrence.Last(DayOfWeek.Thursday);
        /// <summary>
        /// Set a monthly recurrence to occur the last Thursday of the month.
        /// </summary>
        /// <remarks>
        /// By default the recurrence will occur the selected day at midnight.
        /// The forth may also be the last day of the month. 
        /// For example January, the 25th is the fourth and last Tuesday of the month.
        /// When enumerated the 25th will appear twice if the monthly recurrence is configured to occur the fourth and the last day of the moth.
        /// </remarks>
        /// <param name="recurrence">The weekly occurrence of a monthly recurrence to be configured.</param>
        /// <returns><see cref="IHourly"/> to fluently configure the daily occurrence.</returns>
        public static IHourly LastFriday(this IWeekly recurrence) => recurrence.Last(DayOfWeek.Friday);
        /// <summary>
        /// Set a monthly recurrence to occur the last Friday of the month.
        /// </summary>
        /// <remarks>
        /// By default the recurrence will occur the selected day at midnight.
        /// The forth may also be the last day of the month. 
        /// For example January, the 25th is the fourth and last Tuesday of the month.
        /// When enumerated the 25th will appear twice if the monthly recurrence is configured to occur the fourth and the last day of the moth.
        /// </remarks>
        /// <param name="recurrence">The weekly occurrence of a monthly recurrence to be configured.</param>
        /// <returns><see cref="IHourly"/> to fluently configure the daily occurrence.</returns>
        public static IHourly LastSaturday(this IWeekly recurrence) => recurrence.Last(DayOfWeek.Saturday);
        /// <summary>
        /// Set a monthly recurrence to occur the first <paramref name="dayOfWeek"/> of the month.
        /// </summary>
        /// <remarks>
        /// By default the recurrence will occur the selected day at midnight.
        /// </remarks>
        /// <param name="recurrence">The weekly occurrence of a monthly recurrence to be configured.</param>
        /// <param name="dayOfWeek">The day of the week.</param>
        /// <returns><see cref="IHourly"/> to fluently configure the daily occurrence.</returns>
        public static IHourly First(this IWeekly recurrence, DayOfWeek dayOfWeek) => recurrence.SetDayInMonth(DayInMonth.First, dayOfWeek);
        /// <summary>
        /// Set a monthly recurrence to occur the second <paramref name="dayOfWeek"/> of the month.
        /// </summary>
        /// <remarks>
        /// By default the recurrence will occur the selected day at midnight.
        /// </remarks>
        /// <param name="recurrence">The weekly occurrence of a monthly recurrence to be configured.</param>
        /// <param name="dayOfWeek">The day of the week.</param>
        /// <returns><see cref="IHourly"/> to fluently configure the daily occurrence.</returns>
        public static IHourly Second(this IWeekly recurrence, DayOfWeek dayOfWeek) => recurrence.SetDayInMonth(DayInMonth.Second, dayOfWeek);
        /// <summary>
        /// Set a monthly recurrence to occur the third <paramref name="dayOfWeek"/> of the month.
        /// </summary>
        /// <remarks>
        /// By default the recurrence will occur the selected day at midnight.
        /// </remarks>
        /// <param name="recurrence">The weekly occurrence of a monthly recurrence to be configured.</param>
        /// <param name="dayOfWeek">The day of the week.</param>
        /// <returns><see cref="IHourly"/> to fluently configure the daily occurrence.</returns>
        public static IHourly Third(this IWeekly recurrence, DayOfWeek dayOfWeek) => recurrence.SetDayInMonth(DayInMonth.Third, dayOfWeek);
        /// <summary>
        /// Set a monthly recurrence to occur the fourth <paramref name="dayOfWeek"/> of the month.
        /// </summary>
        /// <remarks>
        /// By default the recurrence will occur the selected day at midnight.
        /// The forth may also be the last day of the month. 
        /// For example January, the 25th is the fourth and last Tuesday of the month.
        /// When enumerated the 25th will appear twice if the monthly recurrence is configured to occur the fourth and the last day of the moth.
        /// </remarks>
        /// <param name="recurrence">The weekly occurrence of a monthly recurrence to be configured.</param>
        /// <param name="dayOfWeek">The day of the week.</param>
        /// <returns><see cref="IHourly"/> to fluently configure the daily occurrence.</returns>
        public static IHourly Fourth(this IWeekly recurrence, DayOfWeek dayOfWeek) => recurrence.SetDayInMonth(DayInMonth.Fourth, dayOfWeek);
        /// <summary>
        /// Set a monthly recurrence to occur the last <paramref name="dayOfWeek"/> of the month.
        /// </summary>
        /// <remarks>
        /// By default the recurrence will occur the selected day at midnight.
        /// The forth may also be the last day of the month. 
        /// For example January, the 25th is the fourth and last Tuesday of the month.
        /// When enumerated the 25th will appear twice if the monthly recurrence is configured to occur the fourth and the last day of the moth.
        /// </remarks>
        /// <param name="recurrence">The weekly occurrence of a monthly recurrence to be configured.</param>
        /// <param name="dayOfWeek">The day of the week.</param>
        /// <returns><see cref="IHourly"/> to fluently configure the daily occurrence.</returns>
        public static IHourly Last(this IWeekly recurrence, DayOfWeek dayOfWeek) => recurrence.SetDayInMonth(DayInMonth.Last, dayOfWeek);
        public static IHourly SetDayInMonth(this IWeekly recurrence, DayInMonth dayInMonth, DayOfWeek dayOfWeek)
        {
            var hourly = new Hourly();
            if (recurrence is Weekly weekly)
            {
                var daily = new Daily
                {
                    DayInMonth = dayInMonth,
                    DayOfWeek = dayOfWeek,
                    Monthly = weekly.Monthly,
                };
                hourly.Daily = daily;
                daily.Monthly.InDay.Add(daily);
            }

            return hourly;
        }
        #endregion Monthly
        #region Weekly
        /// <summary>
        /// Creates a weekly recurrence.
        /// </summary>
        /// <remarks>
        /// By default a weekly recurrence will take place every Sunday at midnight.
        /// </remarks>
        /// <returns>
        /// <see cref="IDaily"/> to fluently configure the daily occurrences.
        /// Use <see cref="On"/> to configure more than one daily occurrence.
        /// </returns>
        public static IDaily EveryWeek() => new Daily()
        {
            Weekly = new Weekly()
        };
        /// <summary>
        /// Lets you configure more than one daily occurrence for a weekly recurrence.
        /// <example>
        /// <code>
        /// Occurs.EveryWeek().On(x =>
        /// {
        ///     x.Tuesday();
        ///     x.Friday().At(x =>
        ///     {
        ///         x.Hour(9).Minute(30);
        ///         x.Hour(12);
        ///     });
        /// });
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="recurrence">Daily occurrence to be configured as part of a weekly recurrence.</param>
        /// <param name="builder">Daily configuration builder.</param>
        /// <returns><see cref="IRecurrent"/> that can be enumerated or serialized</returns>
        public static IRecurrent On(this IDaily recurrence, Action<IDaily> builder)
        {
            builder(recurrence);
            return recurrence;
        }
        /// <summary>
        /// Set a weekly recurrence to occur on Sunday.
        /// </summary>
        /// <remarks>
        /// By default the occurrence will be at midnight.
        /// </remarks>
        /// <param name="recurrence">Daily occurrence of a weekly recurrence.</param>
        /// <returns><see cref="IHourly"/> to fluently configure the hourly occurrence.</returns>
        public static IHourly Sunday(this IDaily recurrence) => recurrence.SetDayOfWeek(DayOfWeek.Sunday);
        /// <summary>
        /// Set a weekly recurrence to occur on Monday.
        /// </summary>
        /// <remarks>
        /// By default the occurrence will be at midnight.
        /// </remarks>
        /// <param name="recurrence">Daily occurrence of a weekly recurrence.</param>
        /// <returns><see cref="IHourly"/> to fluently configure the hourly occurrence.</returns>
        public static IHourly Monday(this IDaily recurrence) => recurrence.SetDayOfWeek(DayOfWeek.Monday);
        /// <summary>
        /// Set a weekly recurrence to occur on Tuesday.
        /// </summary>
        /// <remarks>
        /// By default the occurrence will be at midnight.
        /// </remarks>
        /// <param name="recurrence">Daily occurrence of a weekly recurrence.</param>
        /// <returns><see cref="IHourly"/> to fluently configure the hourly occurrence.</returns>
        public static IHourly Tuesday(this IDaily recurrence) => recurrence.SetDayOfWeek(DayOfWeek.Tuesday);
        /// <summary>
        /// Set a weekly recurrence to occur on Wednesday.
        /// </summary>
        /// <remarks>
        /// By default the occurrence will be at midnight.
        /// </remarks>
        /// <param name="recurrence">Daily occurrence of a weekly recurrence.</param>
        /// <returns><see cref="IHourly"/> to fluently configure the hourly occurrence.</returns>
        public static IHourly Wednesday(this IDaily recurrence) => recurrence.SetDayOfWeek(DayOfWeek.Wednesday);
        /// <summary>
        /// Set a weekly recurrence to occur on Thursday.
        /// </summary>
        /// <remarks>
        /// By default the occurrence will be at midnight.
        /// </remarks>
        /// <param name="recurrence">Daily occurrence of a weekly recurrence.</param>
        /// <returns><see cref="IHourly"/> to fluently configure the hourly occurrence.</returns>
        public static IHourly Thursday(this IDaily recurrence) => recurrence.SetDayOfWeek(DayOfWeek.Thursday);
        /// <summary>
        /// Set a weekly recurrence to occur on Friday.
        /// </summary>
        /// <remarks>
        /// By default the occurrence will be at midnight.
        /// </remarks>
        /// <param name="recurrence">Daily occurrence of a weekly recurrence.</param>
        /// <returns><see cref="IHourly"/> to fluently configure the hourly occurrence.</returns>
        public static IHourly Friday(this IDaily recurrence) => recurrence.SetDayOfWeek(DayOfWeek.Friday);
        /// <summary>
        /// Set a weekly recurrence to occur on Saturday.
        /// </summary>
        /// <remarks>
        /// By default the occurrence will be at midnight.
        /// </remarks>
        /// <param name="recurrence">Daily occurrence of a weekly recurrence.</param>
        /// <returns><see cref="IHourly"/> to fluently configure the hourly occurrence.</returns>
        public static IHourly Saturday(this IDaily recurrence) => recurrence.SetDayOfWeek(DayOfWeek.Saturday);
        /// <summary>
        /// Set a weekly recurrence to occur on <paramref name="dayOfWeek"/>.
        /// </summary>
        /// <remarks>
        /// By default the occurrence will be at midnight.
        /// </remarks>
        /// <param name="recurrence">Daily occurrence of a weekly recurrence.</param>
        /// <param name="dayOfWeek">The day of the week to set.</param>
        /// <returns><see cref="IHourly"/> to fluently configure the hourly occurrence.</returns>
        public static IHourly SetDayOfWeek(this IDaily recurrence, DayOfWeek dayOfWeek)
        {
            var hourly = new Hourly();
            if (recurrence is Daily daily)
            {
                hourly.Daily = new Daily
                {
                    Weekly = daily.Weekly,
                    DayOfWeek = dayOfWeek
                };
                daily.Weekly.On.Add(hourly.Daily);
            }

            return hourly;
        }
        #endregion Weekly
        #region Dayly
        /// <summary>
        /// Creates a daily recurrence.
        /// </summary>
        /// <remarks>
        /// By default a daily recurrence will take place every day at midnight.
        /// </remarks>
        /// <returns>
        /// <see cref="IHourly"/> to fluently configure the hourly occurrences.
        /// Use <see cref="At"/> to configure more than one hourly occurrence.
        /// </returns>
        public static IHourly EveryDay() => new Hourly
        {
            Daily = new Daily(),
        };
        /// <summary>
        /// Lets you configure more than one hourly occurrence for a daily recurrence.
        /// <example>
        /// <code>
        /// Occurs.EveryDay().At(x =>
        /// {
        ///     x.Hour(8);
        ///     x.Hour(17).Minute(30);
        ///     x.Hour(19).Minute(40).Second(50);
        /// });
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="recurrence">Hourly occurrence to be configured as part of a weekly recurrence.</param>
        /// <param name="builder">Hourly configuration builder.</param>
        /// <returns><see cref="IRecurrent"/> that can be enumerated or serialized.</returns>
        public static IRecurrent At(this IHourly recurrence, Action<IHourly> builder)
        {
            builder(recurrence);
            return recurrence;
        }
        /// <summary>
        /// Sets the hourly occurrence of a daily recurrence.
        /// </summary>
        /// <param name="recurrence">Hourly occurrence of a daily recurrence.</param>
        /// <param name="hour">The hour of the day form 0 to 23.</param>
        /// <returns><see cref="IMinutely"/> to fluently configure the minute.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The <paramref name="hour"/> must be between 0 and 23.</exception>
        public static IMinutely Hour(this IHourly recurrence, int hour)
        {
            if (hour < 0 || hour > 23)
            {
                throw new ArgumentOutOfRangeException(nameof(hour), hour, "The hour must be between 0 and 23.");
            }

            var minutely = new Minutely();
            if (recurrence is Hourly hourly)
            {
                var hourlyClone = new Hourly
                {
                    Daily = hourly.Daily,
                    Hour = hour,
                    Minutely = minutely
                };
                minutely.Hourly = hourlyClone;
                hourly.Daily.At.Add(hourlyClone);
            }

            return minutely;
        }
        /// <summary>
        /// Sets the minute in which a daily recurrence will occur.
        /// </summary>
        /// <param name="recurrence">The minute of the hour of a daily recurrence.</param>
        /// <param name="minute">The minute of the hour from 0 to 59.</param>
        /// <returns><see cref="ISecondly"/> to fluently configure the second.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The <paramref name="minute"/> must be between 0 and 59.</exception>
        public static ISecondly Minute(this IMinutely recurrence, int minute)
        {
            if (minute < 0 || minute > 59)
            {
                throw new ArgumentOutOfRangeException(nameof(minute), minute, "The minute must be between 0 and 59.");
            }

            var secondly = new Secondly();
            if (recurrence is Minutely minutely)
            {
                minutely.Minute = minute;
                minutely.Secondly = secondly;
                secondly.Minutely = minutely;
            }

            return secondly;
        }
        /// <summary>
        /// Sets the second in which a daily recurrence will occur.
        /// </summary>
        /// <param name="recurrence">The second of a daily recurrence.</param>
        /// <param name="second">The second to set.</param>
        /// <returns><see cref="IRecurrent"/> that can be enumerated or serialized</returns>
        /// <exception cref="ArgumentOutOfRangeException">The second must be between 0 and 59.</exception>
        public static IRecurrent Second(this ISecondly recurrence, int second)
        {
            if (second < 0 || second > 59)
            {
                throw new ArgumentOutOfRangeException(nameof(second), second, "The second must be between 0 and 59.");
            }

            if (recurrence is Secondly secondly)
            {
                secondly.Second = second;
            }

            return recurrence;
        }
        #endregion Dayly
    }
}