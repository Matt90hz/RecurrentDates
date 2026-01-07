using IncaTechnologies.Recurrence;

namespace Tests;

public class VerifyJsonParser(SerializationFixture fixture)
{
    [Fact]
    public Task Daily() => Verify(JsonParser.Parse(fixture.Daily.ToJson()));

    [Fact]
    public Task Weekly() => Verify(JsonParser.Parse(fixture.Weekly.ToJson()));

    [Fact]
    public Task Monthly() => Verify(JsonParser.Parse(fixture.Monthly.ToJson()));

    [Fact]
    public Task Yearly() => Verify(JsonParser.Parse(fixture.Yearly.ToJson()));

}