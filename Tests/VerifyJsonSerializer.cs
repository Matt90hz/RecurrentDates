using IncaTechnologies.Recurrence;
using System.Text;
using System.Text.Json;

namespace Tests;

public class VerifyJsonSerializer(SerializationFixture fixture)
{
    [Fact]
    public Task DailyToJson() => Verify(Occurs.EveryDay().ToJson(fixture.JsonSerializerOptions));

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

    [Fact]
    public Task DailyToStream()
    {
        using var stream = new MemoryStream();
        using var writer = new Utf8JsonWriter(stream, fixture.JsonSerializerOptions.ToJsonWriterOptions());

        Occurs.EveryDay().WriteTo(writer);

        return Verify(Encoding.UTF8.GetString(stream.ToArray()))
            .UseMethodName(nameof(VerifyJsonSerializer.DailyToJson));
    }

    [Fact]
    public Task DailyAtToStream()
    {
        using var stream = new MemoryStream();
        using var writer = new Utf8JsonWriter(stream, fixture.JsonSerializerOptions.ToJsonWriterOptions());

        fixture.Daily.WriteTo(writer);

        return Verify(Encoding.UTF8.GetString(stream.ToArray()))
            .UseMethodName(nameof(VerifyJsonSerializer.DailyAtToJson));
    }

    [Fact]
    public Task WeeklyToStream()
    {
        using var stream = new MemoryStream();
        using var writer = new Utf8JsonWriter(stream, fixture.JsonSerializerOptions.ToJsonWriterOptions());

        Occurs.EveryWeek().WriteTo(writer);

        return Verify(Encoding.UTF8.GetString(stream.ToArray()))
            .UseMethodName(nameof(VerifyJsonSerializer.WeeklyToJson));
    }

    [Fact]
    public Task WeeklyOnToStream()
    {
        using var stream = new MemoryStream();
        using var writer = new Utf8JsonWriter(stream, fixture.JsonSerializerOptions.ToJsonWriterOptions());

        fixture.Weekly.WriteTo(writer);

        return Verify(Encoding.UTF8.GetString(stream.ToArray()))
            .UseMethodName(nameof(VerifyJsonSerializer.WeeklyOnToJson));
    }

    [Fact]
    public Task MonthlyToStream()
    {
        using var stream = new MemoryStream();
        using var writer = new Utf8JsonWriter(stream, fixture.JsonSerializerOptions.ToJsonWriterOptions());

        Occurs.EveryMonth().WriteTo(writer);

        return Verify(Encoding.UTF8.GetString(stream.ToArray()))
            .UseMethodName(nameof(VerifyJsonSerializer.MonthlyToJson));
    }

    [Fact]
    public Task MonthlyTheToStream()
    {
        using var stream = new MemoryStream();
        using var writer = new Utf8JsonWriter(stream, fixture.JsonSerializerOptions.ToJsonWriterOptions());

        fixture.Monthly.WriteTo(writer);

        return Verify(Encoding.UTF8.GetString(stream.ToArray()))
            .UseMethodName(nameof(VerifyJsonSerializer.MonthlyTheToJson));
    }

    [Fact]
    public Task YearlyToStream()
    {
        using var stream = new MemoryStream();
        using var writer = new Utf8JsonWriter(stream, fixture.JsonSerializerOptions.ToJsonWriterOptions());

        Occurs.EveryYear().WriteTo(writer);

        return Verify(Encoding.UTF8.GetString(stream.ToArray()))
            .UseMethodName(nameof(VerifyJsonSerializer.YearlyToJson));
    }

    [Fact]
    public Task YearlyInToStream()
    {
        using var stream = new MemoryStream();
        using var writer = new Utf8JsonWriter(stream, fixture.JsonSerializerOptions.ToJsonWriterOptions());

        fixture.Yearly.WriteTo(writer);

        return Verify(Encoding.UTF8.GetString(stream.ToArray()))
            .UseMethodName(nameof(VerifyJsonSerializer.YearlyInToJson));
    }

}