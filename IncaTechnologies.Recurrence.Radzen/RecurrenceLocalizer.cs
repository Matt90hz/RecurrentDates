using Radzen;
using System.Globalization;

namespace IncaTechnologies.Recurrence.Radzen;

public class RecurrenceLocalizer : ILocalizer
{
    public string? Get(string key, CultureInfo culture) => RecurrenceStrings.ResourceManager.GetString(key, culture);
}
