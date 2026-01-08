using IncaTechnologies.Recurrence;

namespace Tests;

public class VerifyJsonParser(SerializationFixture fixture)
{

    [Fact]
    public Task Daily() => Verify(JsonParser.Parse(fixture.DailyJson));

    [Fact]
    public Task Weekly() => Verify(JsonParser.Parse(fixture.WeeklyJson));

    [Fact]
    public Task Monthly() => Verify(JsonParser.Parse(fixture.MonthlyJson));

    [Fact]
    public Task Yearly() => Verify(JsonParser.Parse(fixture.YearlyJson));
    
    [Fact]
    public Task DailyFromStream()
    {
        var stream = fixture.DailyJsonStream();

        return Verify(JsonParser.Parse(ref stream)).UseMethodName(nameof(VerifyJsonParser.Daily));
    }

    [Fact]
    public Task WeeklyFromStream()
    {
        var stream = fixture.WeeklyJsonStream();

        return Verify(JsonParser.Parse(ref stream)).UseMethodName(nameof(VerifyJsonParser.Weekly));
    }

    [Fact]
    public Task MonthlyFromStream()
    {
        var stream = fixture.MonthlyJsonStream();

        return Verify(JsonParser.Parse(ref stream)).UseMethodName(nameof(VerifyJsonParser.Monthly));
    }

    [Fact]
    public Task YearlyFromStream()
    {
        var stream = fixture.YearlyJsonStream();

        return Verify(JsonParser.Parse(ref stream)).UseMethodName(nameof(VerifyJsonParser.Yearly));
    }
}