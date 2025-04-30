using IncaTechnologies.Recurrence;
using System.Threading.Tasks;

namespace Tests;

public class VerifyOccurs
{
    [Fact]
    public Task EveryDay() => Verify(Occurs.EveryDay() as Hourly);
    [Fact]
    public Task EveryDay_At() => Verify(Occurs.EveryDay().At(x =>
    {
        x.Hour(9);
        x.Hour(12).Minute(15);
        x.Hour(17).Minute(30).Second(25);
    }) as Hourly);
    [Fact]
    public Task EveryWeek() => Verify(Occurs.EveryWeek() as Daily);
    [Fact]
    public Task EveryWeek_On() => Verify(Occurs.EveryWeek().On(x =>
    {
        x.Monday();
        x.Tuesday();
        x.Wednesday();
        x.Thursday().Hour(8);;
        x.Friday().Hour(8).Minute(15);;
        x.Saturday().Hour(8).Minute(15);;
        x.Sunday().Hour(9).Minute(15).Second(30); ;
    }) as Daily);
    [Fact]
    public Task EveryMonth() => Verify(Occurs.EveryMonth() as Weekly);
    [Fact]
    public Task EveryMonth_The() => Verify(Occurs.EveryMonth().The(x =>
    {
        x.Day(15);
        x.Day(20).Hour(10).Minute(15).Second(30);
        x.FirstMonday();
        x.SecondMonday();
        x.ThirdMonday();
        x.FourthMonday();
        x.LastMonday().Hour(9).Minute(30).Second(45); ;
    })as Weekly);
    [Fact]
    public Task EveryYear() => Verify(Occurs.EveryYear() as Monthly);
    [Fact]
    public Task EveryYear_In() => Verify(Occurs.EveryYear().In(x =>
    {
        x.January();
        x.February();
        x.March();
        x.May();
        x.June();
        x.July();
        x.August();
        x.September();
        x.October();
        x.November().SecondMonday();
        x.December().Day(15);
    }) as Monthly);
}
