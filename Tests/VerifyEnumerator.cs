using IncaTechnologies.Recurrence;

namespace Tests;

public class VerifyEnumerator
{
    #region Daily
    [Fact]
    public Task EveryDay() => Verify(Occurs.EveryDay()
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2000, 1, 3)));
    [Fact]
    public Task EveryDayAt() => Verify(
        Occurs.EveryDay().At(x =>
        {
            x.Hour(9);
            x.Hour(12).Minute(15);
            x.Hour(17).Minute(30).Second(25);
        })
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2000, 1, 3)));
    #endregion
    #region Weekly
    [Fact]
    public Task EveryWeek() => Verify(Occurs.EveryWeek()
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2000, 1, 31)));
    [Fact]
    public Task EveryWeekOn() => Verify(
        Occurs.EveryWeek().On(x =>
        {
            x.Monday();
            x.Wednesday().Hour(9).Minute(15).Second(30);
        })
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2000, 1, 31)));
    [Fact]
    public Task EveryWeekOnMonday() => Verify(
        Occurs.EveryWeek().Monday()
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2000, 1, 31)));
    [Fact]
    public Task EveryWeekOnTuesday() => Verify(
        Occurs.EveryWeek().Tuesday()
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2000, 1, 31)));
    [Fact]
    public Task EveryWeekOnWednesday() => Verify(
        Occurs.EveryWeek().Wednesday()
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2000, 1, 31)));
    [Fact]
    public Task EveryWeekOnThursday() => Verify(
        Occurs.EveryWeek().Thursday()
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2000, 1, 31)));
    [Fact]
    public Task EveryWeekOnFriday() => Verify(
        Occurs.EveryWeek().Friday()
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2000, 1, 31)));
    [Fact]
    public Task EveryWeekOnSaturday() => Verify(
        Occurs.EveryWeek().Saturday()
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2000, 1, 31)));
    [Fact]
    public Task EveryWeekOnSunday() => Verify(
        Occurs.EveryWeek().Sunday()
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2000, 1, 31)));
    #endregion
    #region Monthly
    [Fact]
    public Task EveryMonth() => Verify(Occurs.EveryMonth()
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2000, 3, 1)));
    [Fact]
    public Task EveryMonthThe() => Verify(
        Occurs.EveryMonth().The(x =>
        {
            x.Day(31);
            x.Day(15).Hour(10).Minute(15).Second(30);
            x.SecondTuesday().Hour(9).Minute(20).Second(45);
            x.LastFriday();
        })
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2000, 3, 1)));
    [Fact]
    public Task EveryMonthTheFirstMonday() => Verify(
        Occurs.EveryMonth().FirstMonday()
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2000, 3, 1)));
    [Fact]
    public Task EveryMonthTheSecondMonday() => Verify(
        Occurs.EveryMonth().SecondMonday()
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2000, 3, 1)));
    [Fact]
    public Task EveryMonthTheThirdMonday() => Verify(
        Occurs.EveryMonth().ThirdMonday()
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2000, 3, 1)));
    [Fact]
    public Task EveryMonthTheFourthMonday() => Verify(
        Occurs.EveryMonth().FourthMonday()
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2000, 3, 1)));
    [Fact]
    public Task EveryMonthTheLastMonday() => Verify(
        Occurs.EveryMonth().LastMonday()
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2000, 3, 1)));
    [Fact]
    public Task EveryMonthTheFirstTuesday() => Verify(
        Occurs.EveryMonth().FirstTuesday()
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2000, 3, 1)));
    [Fact]
    public Task EveryMonthTheSecondTuesday() => Verify(
        Occurs.EveryMonth().SecondTuesday()
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2000, 3, 1)));
    [Fact]
    public Task EveryMonthTheThirdTuesday() => Verify(
        Occurs.EveryMonth().ThirdTuesday()
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2000, 3, 1)));
    [Fact]
    public Task EveryMonthTheFourthTuesday() => Verify(
        Occurs.EveryMonth().FourthTuesday()
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2000, 3, 1)));
    [Fact]
    public Task EveryMonthTheLastTuesday() => Verify(
        Occurs.EveryMonth().LastTuesday()
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2000, 3, 1)));
    [Fact]
    public Task EveryMonthTheFirstWednesday() => Verify(
        Occurs.EveryMonth().FirstWednesday()
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2000, 3, 1)));
    [Fact]
    public Task EveryMonthTheSecondWednesday() => Verify(
        Occurs.EveryMonth().SecondWednesday()
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2000, 3, 1)));
    [Fact]
    public Task EveryMonthTheThirdWednesday() => Verify(
        Occurs.EveryMonth().ThirdWednesday()
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2000, 3, 1)));
    [Fact]
    public Task EveryMonthTheFourthWednesday() => Verify(
        Occurs.EveryMonth().FourthWednesday()
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2000, 3, 1)));
    [Fact]
    public Task EveryMonthTheLastWednesday() => Verify(
        Occurs.EveryMonth().LastWednesday()
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2000, 3, 1)));
    [Fact]
    public Task EveryMonthTheFirstThursday() => Verify(
        Occurs.EveryMonth().FirstThursday()
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2000, 3, 1)));
    [Fact]
    public Task EveryMonthTheSecondThursday() => Verify(
        Occurs.EveryMonth().SecondThursday()
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2000, 3, 1)));
    [Fact]
    public Task EveryMonthTheThirdThursday() => Verify(
        Occurs.EveryMonth().ThirdThursday()
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2000, 3, 1)));
    [Fact]
    public Task EveryMonthTheFourthThursday() => Verify(
        Occurs.EveryMonth().FourthThursday()
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2000, 3, 1)));
    [Fact]
    public Task EveryMonthTheLastThursday() => Verify(
        Occurs.EveryMonth().LastThursday()
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2000, 3, 1)));
    [Fact]
    public Task EveryMonthTheFirstFriday() => Verify(
        Occurs.EveryMonth().FirstFriday()
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2000, 3, 1)));
    [Fact]
    public Task EveryMonthTheSecondFriday() => Verify(
        Occurs.EveryMonth().SecondFriday()
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2000, 3, 1)));
    [Fact]
    public Task EveryMonthTheThirdFriday() => Verify(
        Occurs.EveryMonth().ThirdFriday()
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2000, 3, 1)));
    [Fact]
    public Task EveryMonthTheFourthFriday() => Verify(
        Occurs.EveryMonth().FourthFriday()
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2000, 3, 1)));
    [Fact]
    public Task EveryMonthTheLastFriday() => Verify(
        Occurs.EveryMonth().LastFriday()
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2000, 3, 1)));
    [Fact]
    public Task EveryMonthTheFirstSaturday() => Verify(
        Occurs.EveryMonth().FirstSaturday()
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2000, 3, 1)));
    [Fact]
    public Task EveryMonthTheSecondSaturday() => Verify(
        Occurs.EveryMonth().SecondSaturday()
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2000, 3, 1)));
    [Fact]
    public Task EveryMonthTheThirdSaturday() => Verify(
        Occurs.EveryMonth().ThirdSaturday()
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2000, 3, 1)));
    [Fact]
    public Task EveryMonthTheFourthSaturday() => Verify(
        Occurs.EveryMonth().FourthSaturday()
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2000, 3, 1)));
    [Fact]
    public Task EveryMonthTheLastSaturday() => Verify(
        Occurs.EveryMonth().LastSaturday()
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2000, 3, 1)));
    [Fact]
    public Task EveryMonthTheFirstSunday() => Verify(
        Occurs.EveryMonth().FirstSunday()
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2000, 3, 1)));
    [Fact]
    public Task EveryMonthTheSecondSunday() => Verify(
        Occurs.EveryMonth().SecondSunday()
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2000, 3, 1)));
    [Fact]
    public Task EveryMonthTheThirdSunday() => Verify(
        Occurs.EveryMonth().ThirdSunday()
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2000, 3, 1)));
    [Fact]
    public Task EveryMonthTheFourthSunday() => Verify(
        Occurs.EveryMonth().FourthSunday()
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2000, 3, 1)));
    [Fact]
    public Task EveryMonthTheLastSunday() => Verify(
        Occurs.EveryMonth().LastSunday()
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2000, 3, 1)));
    #endregion
    #region Yearly
    [Fact]
    public Task EveryYear() => Verify(Occurs.EveryYear()
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2005, 1, 1)));
    [Fact]
    public Task EveryYearIn() => Verify(Occurs.EveryYear()
       .In(x =>
        {
            x.April();
            x.June().Day(15);
            x.September().FourthFriday();
        })
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2005, 1, 1)));
    [Fact]
    public Task EveryYearInJanuary() => Verify(
        Occurs.EveryYear().January()
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2005, 1, 1)));
    [Fact]
    public Task EveryYearInFebruary() => Verify(
        Occurs.EveryYear().February()
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2005, 1, 1)));
    [Fact]
    public Task EveryYearInMarch() => Verify(
        Occurs.EveryYear().March()
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2005, 1, 1)));
    [Fact]
    public Task EveryYearInMay() => Verify(
        Occurs.EveryYear().May()
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2005, 1, 1)));
    [Fact]
    public Task EveryYearInApril() => Verify(
        Occurs.EveryYear().April()
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2005, 1, 1)));
    [Fact]
    public Task EveryYearInJune() => Verify(
        Occurs.EveryYear().June()
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2005, 1, 1)));
    [Fact]
    public Task EveryYearInJuly() => Verify(
        Occurs.EveryYear().July()
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2005, 1, 1)));
    [Fact]
    public Task EveryYearInAugust() => Verify(
        Occurs.EveryYear().August()
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2005, 1, 1)));
    [Fact]
    public Task EveryYearInSeptember() => Verify(
        Occurs.EveryYear().September()
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2005, 1, 1)));
    [Fact]
    public Task EveryYearInOctober() => Verify(
        Occurs.EveryYear().October()
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2005, 1, 1)));
    [Fact]
    public Task EveryYearInNovember() => Verify(
        Occurs.EveryYear().November()
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2005, 1, 1)));
    [Fact]
    public Task EveryYearInDecember() => Verify(
        Occurs.EveryYear().December()
        .AsEnumerable(new DateTime(2000, 1, 1), new DateTime(2005, 1, 1)));
    #endregion
}