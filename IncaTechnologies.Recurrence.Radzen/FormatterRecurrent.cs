using System.Text;

namespace IncaTechnologies.Recurrence.Radzen;

public static class FormatterRecurrent
{
    internal static readonly string[] Mesi = ["Gennaio", "Febbraio", "Marzo", "Aprile", "Maggio", "Giugno", "Luglio", "Agosto", "Settembre", "Ottobre", "Novembre", "Dicembre"];
    internal static readonly string[] SettimaneNelMese = ["Prima", "Seconda", "Terza", "Quarta", "Ultima"];
    internal static readonly string[] GiorniDellaSettimana = ["Lunedì", "Martedì", "Mercoledì", "Giovedì", "Venerdì", "Sabato", "Domenica"];

    public static string ToFriendlyString(this IRecurrent recurrent)
    {
        var sb = new StringBuilder();

        sb = recurrent.GetRoot() switch
        {
            IYearly => sb.Append("Ogni anno"),
            IMonthly => sb.Append("Ogni mese"),
            IWeekly => sb.Append("Ogni settimana"),
            IDaily => sb.Append("Ogni giorno"),
            _ => sb
        };

        sb.AppendRecurrent(recurrent);
        sb.Append('.');

        return sb.ToString();
    }

    private static StringBuilder AppendRecurrent(this StringBuilder sb, IRecurrent recurrent)
    {
        _ = recurrent switch
        {
            IYearly y => y.ForEachIn(x =>
            {
                sb.Append(", in ");
                sb.Append(Mesi[x.Month - 1]);
                sb.AppendRecurrent(x.Then);
            }),
            IMonthly m => m.ForEachThe(x =>
            {
                sb.Append(", il ");
                sb.Append(x.DayOfMonth);
                sb.AppendRecurrent(x.Then);

            }, x =>
            {
                sb.Append(", ");
                sb.Append(x.DayOfWeek.ToFriendlyString().ToLower());
                sb.Append(" della ");
                sb.Append(x.DayInMonth.ToFriendlyString().ToLower());
                sb.Append(" settimana");
                sb.AppendRecurrent(x.Then);
            }),
            IWeekly w => w.ForEachOn(x =>
            {
                sb.Append(", di ");
                sb.Append(x.DayOfWeek.ToFriendlyString());
                sb.AppendRecurrent(x.Then);
            }),
            IDaily d => d.ForEachAt(x =>
            {
                sb.Append(", alle ");
                sb.Append(x.Hour.ToString("00"));
                sb.Append(':');
                sb.Append(x.Minute.ToString("00"));
            }),
            _ => recurrent,
        };

        return sb;
    }

    internal static string ToFriendlyString(this DayInMonth dayInMonth)
    {
        return dayInMonth switch
        {
            DayInMonth.First => "Prima",
            DayInMonth.Second => "Seconda",
            DayInMonth.Third => "Terza",
            DayInMonth.Fourth => "Quarta",
            DayInMonth.Last => "Ultima",
            _ => string.Empty
        };
    }

    internal static string ToFriendlyString(this DayOfWeek dayOfWeek)
    {
        return dayOfWeek switch
        {
            DayOfWeek.Monday => "Lunedì",
            DayOfWeek.Tuesday => "Martedì",
            DayOfWeek.Wednesday => "Mercoledì",
            DayOfWeek.Thursday => "Giovedì",
            DayOfWeek.Friday => "Venerdì",
            DayOfWeek.Saturday => "Sabato",
            DayOfWeek.Sunday => "Domenica",
            _ => string.Empty
        };
    }
}
