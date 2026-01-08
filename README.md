# Incatechnologies Recurrence

Welcome to the Incatechnologies Recurrence Library, a powerful and flexible solution for handling recurring dates in your .NET projects. Whether you need to manage event schedules, automate reminders, or create complex recurrence patterns, this library has you covered.

## Latest

1.0.2 - Added a practical implementation of `JsonConverter` to use in your projects.

## Create a recurrence

The available recurrences are daily, weekly, monthly, and yearly. All the recurrence builders are part of the `Occurs` class, along with helper functions that allow you to build recurrences fluently.

### Examples
```CSharp
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
```
## Enumerate a recurrence
Once a recurrence is created, it can be enumerated. This means that, given a specified period, the enumeration will return all the dates on which the recurrence occurs.
### Example
```CSharp
IEnumerable<DateTime> occurrences = Occurs.EveryDay()
    .AsEnumerable(
        DateTime.Parse("01/01/2000"),
        DateTime.Parse("01/12/2000"));
```
## Serialize and parse
Recurrences can be serialized to JSON and parsed from JSON. This is especially convenient when the recurrence is part of an application that needs to store the recurrence data in a file, a database, or exchange it in the context of a web API.

**The `System.Text.Json` library is used behind the scenes for serialization and parsing.**

### Example
```CSharp
var options = new JsonSerializerOptions() { WriteIndented = true };
string json = Occurs.EveryDay().ToJson(options);
IRecurrent recurrent = JsonParser.Parse(json);
```
## Mapping and iteration
Mapping and iterating allow you to transform a recurrence into another form, such as a view model, a request, a response, or anything else you might need.

Recurrences can be read in a similar way to how they are built. The structure follows a hierarchy where one parent recurrence can contain one or many child occurrence elements. It is important to start reading recurrences from the topmost element down to ensure the entire structure is captured. The `GetRoot` function can help with this.
## Example
```CSharp
// Extract data from a recurrence
// Iteration
var vm = new VieModel();
(Occurs.EveryYear().GetRoot() as IYeraly)
    .ForEachIn(x => 
    { 
        vm.Month = x.Month;
        x.Then.ForEachThe(
            x => vm.Day.Add(x.DayOfMonth), 
            x => vm.WeekDay.Add(x.DayOfWeek)); 
    });

// Mapping
var mapped = (Occurs.EveryYear().GetRoot() as IYeraly)
    .SelectIn(x => new VieModel
    {
        Month = x.Month,
        Day = [..x.Then.SelectTheDay(x => x.DayOfMonth)],
        WeekDay = [..x.Then.SelectTheWeekDay(x => x.DayOfWeek)],
    });
```
# Contributing
We welcome contributions! If you have ideas, suggestions, or improvements, feel free to open an issue or submit a pull request. 
