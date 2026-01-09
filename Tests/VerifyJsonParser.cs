using IncaTechnologies.Recurrence;
using System.Text;
using System.Text.Json;


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
        Utf8JsonReader stream = new(
            Encoding.UTF8.GetBytes(fixture.DailyJson),
            fixture.JsonSerializerOptions.ToJsonReaderOptions());

        return Verify(JsonParser.Parse(ref stream)).UseMethodName(nameof(VerifyJsonParser.Daily));
    }

    [Fact]
    public Task WeeklyFromStream()
    {
        Utf8JsonReader stream =new(
            Encoding.UTF8.GetBytes(fixture.WeeklyJson),
            fixture.JsonSerializerOptions.ToJsonReaderOptions());

        return Verify(JsonParser.Parse(ref stream)).UseMethodName(nameof(VerifyJsonParser.Weekly));
    }

    [Fact]
    public Task MonthlyFromStream()
    {
        Utf8JsonReader stream =new(
            Encoding.UTF8.GetBytes(fixture.MonthlyJson),
            fixture.JsonSerializerOptions.ToJsonReaderOptions());

        return Verify(JsonParser.Parse(ref stream)).UseMethodName(nameof(VerifyJsonParser.Monthly));
    }

    [Fact]
    public Task YearlyFromStream()
    {
        Utf8JsonReader stream =new(
            Encoding.UTF8.GetBytes(fixture.YearlyJson),
            fixture.JsonSerializerOptions.ToJsonReaderOptions());

        return Verify(JsonParser.Parse(ref stream)).UseMethodName(nameof(VerifyJsonParser.Yearly));
    }
}