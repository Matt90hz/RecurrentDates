using IncaTechnologies.Recurrence;
using System.Text;
using System.Text.Json;
using Recurrence = IncaTechnologies.Recurrence;

// Every day at midnight.
var a = Occurs.EveryDay();

// Every day at 9:30.
var b = Occurs.EveryDay().Hour(9).Minute(30);

// Every day at 8:00, 17:30 and 19:40:50.
var c = Occurs.EveryDay().At(x =>
{
    x.Hour(8);
    x.Hour(17).Minute(30);
    x.Hour(19).Minute(40).Second(50);
});

// Every week, Sunday midnight.
var d = Occurs.EveryWeek();

// Every week, Tuesday at 12:00
var e = Occurs.EveryWeek().Tuesday().Hour(12);

// Every week, Tuesday at midnight and Friday at 9:30 and 12:00
var f = Occurs.EveryWeek().On(x =>
{
    x.Tuesday();
    x.Friday().At(x =>
    {
        x.Hour(9).Minute(30);
        x.Hour(12);
    });
});

// Every first of the month at midnight
var g = Occurs.EveryMonth();

// Every 15 of the month at 12:00
var h = Occurs.EveryMonth().Day(15).Hour(12);

// Every second Wednesday of the month
var j = Occurs.EveryMonth().SecondWednesday();

// Every 1 and second Friday of the month at 18:30.
var i = Occurs.EveryMonth().The(x =>
{
    x.Day(1);
    x.SecondFriday().Hour(18).Minute(30);
});

// Every year the first of January at midnight
var k = Occurs.EveryYear();

// Every year in June
var l = Occurs.EveryYear().June();

// All Combined
var m = Occurs.EveryYear().In(x =>
{
    x.May();
    x.April().The(x =>
    {
        x.Day(15);
        x.Day(16).At(x =>
        {
            x.Hour(10);
            x.Hour(12);
        });
        x.FirstMonday();
        x.SecondFriday().At(x =>
        {
            x.Hour(9).Minute(30);
            x.Hour(12).Minute(45).Second(20);
        });
    });
});

// Serializing and Parsing
var options = new JsonSerializerOptions() { WriteIndented = true };
var json = m.ToJson(options);
IRecurrent recurrent = Recurrence.JsonParser.Parse(json);

// Enumerating
IEnumerable<DateTime> occurrences = Occurs.EveryDay()
    .AsEnumerable(
        DateTime.Parse("01/01/2000"),
        DateTime.Parse("01/12/2000"));

// Walk around for every other year or more than yearly recurrence
// If the usage justifies it, or upon request, other recurrence will be implemented
var everyOtherYear = Occurs
    .EveryYear()
    .AsEnumerable(DateTime.Now, DateTime.Now)
    .Where(d => d.Year % 2 == 0); // Of course if you want to convert it in Json you also must have some custom logic for

// Extract data from a recurrence
// Iteration
var vm = new VieModel();
((IYearly)m.GetRoot())
    .ForEachIn(x => 
    { 
        vm.Month = x.Month;
        x.Then.ForEachThe(
            x => vm.Day.Add(x.DayOfMonth), 
            x => vm.WeekDay.Add(x.DayOfWeek)); 
    });

// Mapping
var mapped = ((IYearly)m.GetRoot())
    .SelectIn(x => new VieModel
    {
        Month = x.Month,
        Day = [..x.Then.SelectTheDay(x => x.DayOfMonth)],
        WeekDay = [..x.Then.SelectTheWeekDay(x => x.DayOfWeek)],
    });

Console.WriteLine();

record VieModel
{
    public int Month { get; set; }
    public List<int> Day { get; set; } = [];
    public List<DayOfWeek> WeekDay { get; set; } = [];
}
