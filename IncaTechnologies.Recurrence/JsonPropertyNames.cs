using System.Collections.Generic;

internal static class JsonPropertyNames
{
    internal const string HOURLY_KEY = "Hour";
    internal const string MINUTELY_KEY = "Minute";
    internal const string SECONDLY_KEY = "Second";
    internal const string DAILY_KEY = "Daily";
    internal const string DAILY_AT_KEY = "At";
    internal const string WEEKLY_KEY = "Weekly";
    internal const string WEEKLY_ON_KEY = "On";
    internal const string MONTHLY_KEY = "Monthly";
    internal const string MONTHLY_THE_KEY = "The";
    internal const string YEARLY_KEY = "Yearly";
    internal const string YEARLY_IN_KEY = "In";

    internal static readonly IReadOnlyDictionary<string, int> MonthNumber = new Dictionary<string, int>()
    {
        ["January"] = 1,
        ["February"] = 2,
        ["March"] = 3,
        ["April"] = 4,
        ["May"] = 5,
        ["June"] = 6,
        ["July"] = 7,
        ["August"] = 8,
        ["September"] = 9,
        ["October"] = 10,
        ["November"] = 11,
        ["December"] = 12,
    };

    internal static readonly IReadOnlyDictionary<int, string> MonthName = new Dictionary<int, string>()
    {
        [1] = "January",
        [2] = "February",
        [3] = "March",
        [4] = "April",
        [5] = "May",
        [6] = "June",
        [7] = "July",
        [8] = "August",
        [9] = "September",
        [10] = "October",
        [11] = "November",
        [12] = "December",
    };
}