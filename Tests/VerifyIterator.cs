using IncaTechnologies.Recurrence;
using System.Text;

namespace Tests;

public class VerifyIterator
{

    [Fact]
    public Task ForEachIn()
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
        var sb = new StringBuilder();
        (recurrence.GetRoot() as IYearly)
            .ForEachIn(@in =>
            {
                @in.Then.ForEachThe(
                    the =>
                    {
                        the.Then.ForEachAt(at =>
                        {
                            sb.AppendJoin(',', @in.Month, the.DayOfMonth, at.Hour, at.Minute, at.Second);
                            sb.AppendLine();
                        });
                    },
                    the =>
                    {
                        the.Then.ForEachAt(at =>
                        {
                            sb.AppendJoin(',', @in.Month, the.DayInMonth, the.DayOfWeek, at.Hour, at.Minute, at.Second);
                            sb.AppendLine();
                        });
                    });
            });
        return Verify(sb.ToString());
    }
    [Fact]
    public Task ForEachThe()
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
        var sb = new StringBuilder();
        (recurrence.GetRoot() as IMonthly)
            .ForEachThe(
                the =>
                {
                    the.Then.ForEachAt(at =>
                    {
                        sb.AppendJoin(',', the.DayOfMonth, at.Hour, at.Minute, at.Second);
                        sb.AppendLine();
                    });
                },
                the =>
                {
                    the.Then.ForEachAt(at =>
                    {
                        sb.AppendJoin(',', the.DayInMonth, the.DayOfWeek, at.Hour, at.Minute, at.Second);
                        sb.AppendLine();
                    });
                });
        return Verify(sb.ToString());
    }
    [Fact]
    public Task ForEachTheDay()
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
        var sb = new StringBuilder();
        (recurrence.GetRoot() as IMonthly)
            .ForEachTheDay(the =>
            {
                the.Then.ForEachAt(at =>
                {
                    sb.AppendJoin(',', the.DayOfMonth, at.Hour, at.Minute, at.Second);
                    sb.AppendLine();
                });
            });
        return Verify(sb.ToString());
    }
    [Fact]
    public Task ForEachTheWeekDay()
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
        var sb = new StringBuilder();
        (recurrence.GetRoot() as IMonthly)
            .ForEachTheWeekDay(the =>
            {
                the.Then.ForEachAt(at =>
                {
                    sb.AppendJoin(',', the.DayInMonth, the.DayOfWeek, at.Hour, at.Minute, at.Second);
                    sb.AppendLine();
                });
            });
        return Verify(sb.ToString());
    }
    [Fact]
    public Task ForEachOn()
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
        var sb = new StringBuilder();
        (recurrence.GetRoot() as IWeekly)
            .ForEachOn(on =>
            {
                on.Then.ForEachAt(at =>
                {
                    sb.AppendJoin(',', on.DayOfWeek, at.Hour, at.Minute, at.Second);
                    sb.AppendLine();
                });
            });
        return Verify(sb.ToString());
    }
    [Fact]
    public Task ForEachAt()
    {
        var recurrence = Occurs.EveryDay().At(x =>
        {
            x.Hour(10).Minute(15).Second(30);
            x.Hour(12).Minute(45);
            x.Hour(18);
        });
        var sb = new StringBuilder();
        (recurrence.GetRoot() as IDaily)
            .ForEachAt(x =>
            {
                sb.AppendJoin(',', x.Hour, x.Minute, x.Second);
                sb.AppendLine();
            });
        return Verify(sb.ToString());
    }
}