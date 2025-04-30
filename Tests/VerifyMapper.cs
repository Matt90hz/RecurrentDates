using IncaTechnologies.Recurrence;

namespace Tests;

public class VerifyMapper
{
    [Fact]
    public Task SelectAt()
    {
        var recurrence = Occurs.EveryDay().At(x =>
        {
            x.Hour(10).Minute(15).Second(30);
            x.Hour(12).Minute(45);
            x.Hour(18);
        });
        var daily = recurrence.GetRoot() as IDaily;
        return Verify(daily.SelectAt(x => x));
    }
    [Fact]
    public Task SelectOn()
    {
        var recurrence = Occurs.EveryWeek().On(x =>
        {
            x.Tuesday();
            x.Friday().At(x =>
            {
                x.Hour(10).Minute(15).Second(30);
                x.Hour(12).Minute(45);
                x.Hour(18);
            });
        });
        var weekly = recurrence.GetRoot() as IWeekly;
        return Verify(weekly.SelectOn(on => on.Then.SelectAt(at => new { on.DayOfWeek, at.Hour, at.Minute, at.Second })));
    }
    [Fact]
    public Task SelectThe()
    {
        var recurrence = Occurs.EveryMonth().The(x =>
        {
            x.Day(12);
            x.SecondFriday().At(x =>
            {
                x.Hour(10).Minute(15).Second(30);
                x.Hour(12).Minute(45);
                x.Hour(18);
            });
        });
        var monthly = recurrence.GetRoot() as IMonthly;
        return Verify(monthly.SelectThe(
            the => the.Then.SelectAt(at => new { Occurrence = the.DayOfMonth.ToString(), at.Hour, at.Minute, at.Second }),
            the => the.Then.SelectAt(at => new { Occurrence = $"{the.DayInMonth}-{the.DayOfWeek}", at.Hour, at.Minute, at.Second })));
    }
    [Fact]
    public Task SelectTheDay()
    {
        var recurrence = Occurs.EveryMonth().The(x =>
        {
            x.Day(12);
            x.SecondFriday().At(x =>
            {
                x.Hour(10).Minute(15).Second(30);
                x.Hour(12).Minute(45);
                x.Hour(18);
            });
        });
        var monthly = recurrence.GetRoot() as IMonthly;
        return Verify(monthly
            .SelectTheDay(the => the.Then
                .SelectAt(at => new { Occurrence = the.DayOfMonth.ToString(), at.Hour, at.Minute, at.Second })));
    }
    [Fact]
    public Task SelectTheWeekDay()
    {
        var recurrence = Occurs.EveryMonth().The(x =>
        {
            x.Day(12);
            x.SecondFriday().At(x =>
            {
                x.Hour(10).Minute(15).Second(30);
                x.Hour(12).Minute(45);
                x.Hour(18);
            });
        });
        var monthly = recurrence.GetRoot() as IMonthly;
        return Verify(monthly
            .SelectTheWeekDay(the => the.Then
                .SelectAt(at => new { Occurrence = $"{the.DayInMonth}-{the.DayOfWeek}", at.Hour, at.Minute, at.Second })));
    }
    [Fact]
    public Task SelectIn()
    {
        var recurrence = Occurs.EveryYear().In(x =>
        {
            x.February();
            x.May().The(x =>
            {
                x.Day(12);
                x.SecondFriday().At(x =>
                {
                    x.Hour(10).Minute(15).Second(30);
                    x.Hour(12).Minute(45);
                    x.Hour(18);
                });
            });
        });
        var yearly = recurrence.GetRoot() as IYearly;
        return Verify(yearly
            .SelectIn(@in => @in.Then.SelectThe(
                the => the.Then.SelectAt(at => new
                {
                    @in.Month,
                    Occurrence = the.DayOfMonth.ToString(),
                    at.Hour,
                    at.Minute,
                    at.Second
                }),
                the => the.Then.SelectAt(at => new
                {
                    @in.Month,
                    Occurrence = $"{the.DayInMonth}-{the.DayOfWeek}",
                    at.Hour,
                    at.Minute,
                    at.Second
                }))));
    }
}