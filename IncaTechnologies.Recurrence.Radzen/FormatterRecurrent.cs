using Radzen;
using System.Globalization;
using System.Text;

namespace IncaTechnologies.Recurrence.Radzen;

/// <summary>
/// Provides friendly, localized text representations of recurrence definitions.
/// </summary>
public static class FormatterRecurrent
{
    /// <summary>
    /// Formats a recurrence definition as a human-readable sentence.
    /// </summary>
    /// <param name="recurrent">The recurrence definition to format.</param>
    /// <param name="localizer">The optional localizer used to translate recurrence terms.</param>
    /// <returns>A human-readable description of the recurrence, ending with a period.</returns>
    public static string ToFriendlyString(this IRecurrent recurrent, ILocalizer? localizer = null)
    {
        recurrent = recurrent.GetRoot();
        var sb = new StringBuilder();

        sb = recurrent switch
        {
            IYearly => sb.Append($"{Localize(localizer, nameof(RecurrenceStrings.Inca_Every), RecurrenceStrings.Inca_Every)} {Localize(localizer, nameof(RecurrenceStrings.Inca_Year), RecurrenceStrings.Inca_Year).ToLower(CultureInfo.CurrentUICulture)}"),
            IMonthly => sb.Append($"{Localize(localizer, nameof(RecurrenceStrings.Inca_Every), RecurrenceStrings.Inca_Every)} {Localize(localizer, nameof(RecurrenceStrings.Inca_Month), RecurrenceStrings.Inca_Month).ToLower(CultureInfo.CurrentUICulture)}"),
            IWeekly => sb.Append($"{Localize(localizer, nameof(RecurrenceStrings.Inca_Every), RecurrenceStrings.Inca_Every)} {Localize(localizer, nameof(RecurrenceStrings.Inca_Week), RecurrenceStrings.Inca_Week).ToLower(CultureInfo.CurrentUICulture)}"),
            IDaily => sb.Append($"{Localize(localizer, nameof(RecurrenceStrings.Inca_Every), RecurrenceStrings.Inca_Every)} {Localize(localizer, nameof(RecurrenceStrings.Inca_Day), RecurrenceStrings.Inca_Day).ToLower(CultureInfo.CurrentUICulture)}"),
            _ => sb
        };

        sb.AppendRecurrent(recurrent, localizer);
        sb.Append('.');

        return sb.ToString();
    }

    private static StringBuilder AppendRecurrent(this StringBuilder sb, IRecurrent recurrent, ILocalizer? localizer = null)
    {
        _ = recurrent switch
        {
            IYearly y => y.ForEachIn(x =>
            {
                sb.Append(", ");
                sb.Append(Localize(localizer, nameof(RecurrenceStrings.Inca_In), RecurrenceStrings.Inca_In).ToLower(CultureInfo.CurrentUICulture));
                sb.Append(' ');
                sb.Append(x.Month.ToFriendlyString(localizer));
                sb.AppendRecurrent(x.Then, localizer);
            }),
            IMonthly m => m.ForEachThe(x =>
            {
                sb.Append(", ");
                sb.Append(Localize(localizer, nameof(RecurrenceStrings.Inca_The_Male), RecurrenceStrings.Inca_The_Male).ToLower(CultureInfo.CurrentUICulture));
                sb.Append(' ');
                sb.Append(x.DayOfMonth);
                sb.AppendRecurrent(x.Then, localizer);

            }, x =>
            {
                sb.Append(", ");
                sb.Append(Localize(localizer, nameof(RecurrenceStrings.Inca_The_Male), RecurrenceStrings.Inca_The_Male).ToLower(CultureInfo.CurrentUICulture));
                sb.Append(' ');
                sb.Append(x.DayInMonth.ToFriendlyString(localizer).ToLower(CultureInfo.CurrentUICulture));
                sb.Append(' ');
                sb.Append(x.DayOfWeek.ToFriendlyString(localizer).ToLower(CultureInfo.CurrentUICulture));
                sb.AppendRecurrent(x.Then, localizer);
            }),
            IWeekly w => w.ForEachOn(x =>
            {
                sb.Append(", ");
                sb.Append(Localize(localizer, nameof(RecurrenceStrings.Inca_On), RecurrenceStrings.Inca_On).ToLower(CultureInfo.CurrentUICulture));
                sb.Append(' ');
                sb.Append(x.DayOfWeek.ToFriendlyString(localizer).ToLower(CultureInfo.CurrentUICulture));
                sb.AppendRecurrent(x.Then, localizer);
            }),
            IDaily d => d.ForEachAt(x =>
            {
                sb.Append(", ");
                sb.Append(Localize(localizer, nameof(RecurrenceStrings.Inca_At), RecurrenceStrings.Inca_At).ToLower(CultureInfo.CurrentUICulture));
                sb.Append(' ');
                sb.Append(x.Hour.ToString("00"));
                sb.Append(':');
                sb.Append(x.Minute.ToString("00"));
            }),
            _ => recurrent,
        };

        return sb;
    }

    private static string Localize(ILocalizer? localizer, string key, string fallback)
        => localizer?.Get(key, CultureInfo.CurrentUICulture) ?? fallback;

    /// <summary>
    /// Gets the localized name of a month number.
    /// </summary>
    /// <param name="month">The month number, from 1 through 12.</param>
    /// <param name="localizer">The optional localizer used to translate the month name.</param>
    /// <returns>The localized month name, or an empty string for an invalid month number.</returns>
    public static string ToFriendlyString(this int month, ILocalizer? localizer) => month switch
    {
        1 => Localize(localizer, nameof(RecurrenceStrings.Inca_Month_January), RecurrenceStrings.Inca_Month_January),
        2 => Localize(localizer, nameof(RecurrenceStrings.Inca_Month_February), RecurrenceStrings.Inca_Month_February),
        3 => Localize(localizer, nameof(RecurrenceStrings.Inca_Month_March), RecurrenceStrings.Inca_Month_March),
        4 => Localize(localizer, nameof(RecurrenceStrings.Inca_Month_April), RecurrenceStrings.Inca_Month_April),
        5 => Localize(localizer, nameof(RecurrenceStrings.Inca_Month_May), RecurrenceStrings.Inca_Month_May),
        6 => Localize(localizer, nameof(RecurrenceStrings.Inca_Month_June), RecurrenceStrings.Inca_Month_June),
        7 => Localize(localizer, nameof(RecurrenceStrings.Inca_Month_July), RecurrenceStrings.Inca_Month_July),
        8 => Localize(localizer, nameof(RecurrenceStrings.Inca_Month_August), RecurrenceStrings.Inca_Month_August),
        9 => Localize(localizer, nameof(RecurrenceStrings.Inca_Month_September), RecurrenceStrings.Inca_Month_September),
        10 => Localize(localizer, nameof(RecurrenceStrings.Inca_Month_October), RecurrenceStrings.Inca_Month_October),
        11 => Localize(localizer, nameof(RecurrenceStrings.Inca_Month_November), RecurrenceStrings.Inca_Month_November),
        12 => Localize(localizer, nameof(RecurrenceStrings.Inca_Month_December), RecurrenceStrings.Inca_Month_December),
        _ => string.Empty
    };

    /// <summary>
    /// Gets the localized ordinal description of a day within a month.
    /// </summary>
    /// <param name="dayInMonth">The ordinal position of the day in the month.</param>
    /// <param name="localizer">The optional localizer used to translate the ordinal.</param>
    /// <returns>The localized ordinal description, or an empty string for an unsupported value.</returns>
    public static string ToFriendlyString(this DayInMonth dayInMonth, ILocalizer? localizer = null)
    {
        return dayInMonth switch
        {
            DayInMonth.First => Localize(localizer, nameof(RecurrenceStrings.Inca_Ordinal_First), RecurrenceStrings.Inca_Ordinal_First),
            DayInMonth.Second => Localize(localizer, nameof(RecurrenceStrings.Inca_Ordinal_Second), RecurrenceStrings.Inca_Ordinal_Second),
            DayInMonth.Third => Localize(localizer, nameof(RecurrenceStrings.Inca_Ordinal_Third), RecurrenceStrings.Inca_Ordinal_Third),
            DayInMonth.Fourth => Localize(localizer, nameof(RecurrenceStrings.Inca_Ordinal_Fourth), RecurrenceStrings.Inca_Ordinal_Fourth),
            DayInMonth.Last => Localize(localizer, nameof(RecurrenceStrings.Inca_Ordinal_Last), RecurrenceStrings.Inca_Ordinal_Last),
            _ => string.Empty
        };
    }

    /// <summary>
    /// Gets the localized name of a day of the week.
    /// </summary>
    /// <param name="dayOfWeek">The day of the week to format.</param>
    /// <param name="localizer">The optional localizer used to translate the day name.</param>
    /// <returns>The localized day name, or an empty string for an unsupported value.</returns>
    public static string ToFriendlyString(this DayOfWeek dayOfWeek, ILocalizer? localizer = null)
    {
        return dayOfWeek switch
        {
            DayOfWeek.Monday => Localize(localizer, nameof(RecurrenceStrings.Inca_DayOfWeek_Monday), RecurrenceStrings.Inca_DayOfWeek_Monday),
            DayOfWeek.Tuesday => Localize(localizer, nameof(RecurrenceStrings.Inca_DayOfWeek_Tuesday), RecurrenceStrings.Inca_DayOfWeek_Tuesday),
            DayOfWeek.Wednesday => Localize(localizer, nameof(RecurrenceStrings.Inca_DayOfWeek_Wednesday), RecurrenceStrings.Inca_DayOfWeek_Wednesday),
            DayOfWeek.Thursday => Localize(localizer, nameof(RecurrenceStrings.Inca_DayOfWeek_Thursday), RecurrenceStrings.Inca_DayOfWeek_Thursday),
            DayOfWeek.Friday => Localize(localizer, nameof(RecurrenceStrings.Inca_DayOfWeek_Friday), RecurrenceStrings.Inca_DayOfWeek_Friday),
            DayOfWeek.Saturday => Localize(localizer, nameof(RecurrenceStrings.Inca_DayOfWeek_Saturday), RecurrenceStrings.Inca_DayOfWeek_Saturday),
            DayOfWeek.Sunday => Localize(localizer, nameof(RecurrenceStrings.Inca_DayOfWeek_Sunday), RecurrenceStrings.Inca_DayOfWeek_Sunday),
            _ => string.Empty
        };
    }
}
