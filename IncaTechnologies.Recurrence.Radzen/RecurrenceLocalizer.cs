using Radzen;
using System.Globalization;

namespace IncaTechnologies.Recurrence.Radzen;

/// <summary>
/// Provides Radzen with localized recurrence-related resource strings.
/// </summary>
public class RecurrenceLocalizer : ILocalizer
{
    /// <inheritdoc/>
    public string? Get(string key, CultureInfo culture) => RecurrenceStrings.ResourceManager.GetString(key, culture);
}