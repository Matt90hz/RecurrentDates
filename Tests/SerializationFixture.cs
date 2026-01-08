using IncaTechnologies.Recurrence;
using System.Text;
using System.Text.Json;
using Tests;

[assembly: AssemblyFixture(typeof(SerializationFixture))]

namespace Tests;

public class SerializationFixture
{
    public SerializationFixture()
    {
        DailyJson = Daily.ToJson();
        WeeklyJson = Weekly.ToJson();
        MonthlyJson = Monthly.ToJson();
        YearlyJson = Yearly.ToJson();

        JsonSerializerOptionsWithConverter = new JsonSerializerOptions(JsonSerializerOptions);
        JsonSerializerOptionsWithConverter.Converters.Add(new RecurrenceJsonConverter());
    }

    public JsonSerializerOptions JsonSerializerOptions { get; } = new()
    {
        WriteIndented = true,
    };

    public JsonSerializerOptions JsonSerializerOptionsWithConverter { get; }

    public IRecurrent Daily { get; } = Occurs.EveryDay().At(x =>
    {
        x.Hour(10).Minute(15).Second(30);
        x.Hour(17).Minute(30).Second(45);
    });

    public IRecurrent Weekly { get; } = Occurs.EveryWeek().On(x =>
    {
        x.Monday();
        x.Tuesday();
        x.Wednesday();
        x.Thursday();
        x.Friday();
        x.Saturday();
        x.Sunday().Hour(17).Minute(30).Second(45);
    });

    public IRecurrent Monthly { get; } = Occurs.EveryMonth().The(x =>
    {
        x.Day(15);
        x.FirstMonday();
        x.SecondMonday();
        x.ThirdMonday();
        x.FourthMonday();
        x.LastMonday();
        x.FirstTuesday();
        x.SecondTuesday();
        x.ThirdTuesday();
        x.FourthTuesday();
        x.LastTuesday();
        x.FirstWednesday();
        x.SecondWednesday();
        x.ThirdWednesday();
        x.FourthWednesday();
        x.LastWednesday();
        x.FirstThursday();
        x.SecondThursday();
        x.ThirdThursday();
        x.FourthThursday();
        x.LastThursday();
        x.FirstFriday();
        x.SecondFriday();
        x.ThirdFriday();
        x.FourthFriday();
        x.LastFriday();
        x.FirstSaturday();
        x.SecondSaturday();
        x.ThirdSaturday();
        x.FourthSaturday();
        x.LastSaturday();
        x.FirstSunday();
        x.SecondSunday();
        x.ThirdSunday();
        x.FourthSunday();
        x.LastSunday().Hour(17).Minute(30).Second(45);
    });

    public IRecurrent Yearly { get; } = Occurs.EveryYear().In(x =>
    {
        x.January();
        x.February();
        x.March();
        x.April();
        x.May();
        x.June();
        x.July();
        x.August();
        x.September();
        x.October();
        x.November().Day(15);
        x.December().FourthMonday().Hour(9).Minute(15).Second(30);
    });

    public string DailyJson { get; }

    public string WeeklyJson {get;}

    public string MonthlyJson {get;}

    public string YearlyJson { get; }

    public Utf8JsonReader DailyJsonStream() => new(Encoding.UTF8.GetBytes(DailyJson));

    public Utf8JsonReader WeeklyJsonStream() => new(Encoding.UTF8.GetBytes(WeeklyJson));

    public Utf8JsonReader MonthlyJsonStream() => new(Encoding.UTF8.GetBytes(MonthlyJson));

    public Utf8JsonReader YearlyJsonStream() => new(Encoding.UTF8.GetBytes(YearlyJson));
}