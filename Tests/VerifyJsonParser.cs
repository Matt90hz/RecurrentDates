using IncaTechnologies.Recurrence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests;
public class VerifyJsonParser
{
    [Fact]
    public Task Daily() => Verify(JsonParser.Parse(
        Occurs.EveryDay()
            .At(x =>
            {
                x.Hour(10).Minute(15).Second(30);
                x.Hour(17).Minute(30).Second(45);
            }).ToJson()));
    [Fact]
    public Task Weekly() => Verify(JsonParser.Parse(
        Occurs.EveryWeek()
            .On(x =>
            {
                x.Monday();
                x.Tuesday();
                x.Wednesday();
                x.Thursday();
                x.Friday();
                x.Saturday();
                x.Sunday().Hour(17).Minute(30).Second(45);
            }).ToJson()));
    [Fact]
    public Task Monthly() => Verify(JsonParser.Parse(
        Occurs.EveryMonth()
            .The(x =>
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
            }).ToJson()));
    [Fact]
    public Task Yearly() => Verify(JsonParser.Parse(
        Occurs.EveryYear()
            .In(x =>
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
            }).ToJson()));
}