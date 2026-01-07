using IncaTechnologies.Recurrence;
using System.Text.Json;

namespace Tests;

public class VerifyJsonSerializer(SerializationFixture fixture)
{
    [Fact]
    public Task DailyToJason() => Verify(Occurs.EveryDay().ToJson(fixture.JsonSerializerOptions));

    [Fact]
    public Task DailyAtToJson() => Verify(fixture.Daily.ToJson(fixture.JsonSerializerOptions));

    [Fact]
    public Task WeeklyToJson() => Verify(Occurs.EveryWeek().ToJson(fixture.JsonSerializerOptions));

    [Fact]
    public Task WeeklyOnToJson() => Verify(fixture.Weekly.ToJson(fixture.JsonSerializerOptions));

    [Fact]
    public Task MonthlyToJson() => Verify(Occurs.EveryMonth().ToJson(fixture.JsonSerializerOptions));

    [Fact]
    public Task MonthlyTheToJson() => Verify(fixture.Monthly.ToJson(fixture.JsonSerializerOptions));

    [Fact]
    public Task YearlyToJson() => Verify(Occurs.EveryYear().ToJson(fixture.JsonSerializerOptions));

    [Fact]
    public Task YearlyInToJson() => Verify(fixture.Yearly.ToJson(fixture.JsonSerializerOptions));

}