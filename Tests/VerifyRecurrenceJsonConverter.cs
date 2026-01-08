using IncaTechnologies.Recurrence;

namespace Tests;

public class VerifyRecurrenceJsonConverter(SerializationFixture fixture)
{
    [Fact]
    public Task RecurrenceSerialize()
    {
        var json = System.Text.Json.JsonSerializer.Serialize(
            fixture.Yearly, 
            fixture.JsonSerializerOptionsWithConverter);

        return Verify(json)
            .UseTypeName(nameof(VerifyJsonSerializer))
            .UseMethodName(nameof(VerifyJsonSerializer.YearlyInToJson));
    }

    [Fact]
    public Task RecurrenceDeserialize()
    {
        var recurrence = System.Text.Json.JsonSerializer.Deserialize<IRecurrent>(
            fixture.YearlyJson, 
            fixture.JsonSerializerOptionsWithConverter);

        return Verify(recurrence)
            .UseTypeName(nameof(VerifyJsonParser))
            .UseMethodName(nameof(VerifyJsonParser.Yearly));
    }
}
