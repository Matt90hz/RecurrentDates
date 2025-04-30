using IncaTechnologies.Recurrence;

namespace Tests;
public class OccursTests
{
    [Fact]
    public void EveryDay_AtInvalidHour_ShouldThrowArgumentOutOrRangeException()
    {
        // Arrange
        // Act
        static IMinutely setTooBigHour() => Occurs.EveryDay().Hour(25);
        static IMinutely setTooSmallHour() => Occurs.EveryDay().Hour(-1);
        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(setTooBigHour);
        Assert.Throws<ArgumentOutOfRangeException>(setTooSmallHour);
    }
    [Fact]
    public void EveryDay_AtInvalidMinute_ShouldThrowArgumentOutOrRangeException()
    {
        // Arrange
        // Act
        static ISecondly setTooBigMinute() => Occurs.EveryDay().Hour(0).Minute(60);
        static ISecondly setTooSmallMinute() => Occurs.EveryDay().Hour(0).Minute(-1);;
        Assert.Throws<ArgumentOutOfRangeException>(setTooBigMinute);
        Assert.Throws<ArgumentOutOfRangeException>(setTooSmallMinute);
    }
    [Fact]
    public void EveryDay_AtInvalidSecond_ShouldThrowArgumentOutOrRangeException()
    {
        // Arrange
        // Act
        static IRecurrent setTooBigSecond() => Occurs.EveryDay().Hour(0).Minute(0).Second(60);
        static IRecurrent setTooSmallSecond() => Occurs.EveryDay().Hour(0).Minute(0).Second(-1);
        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(setTooBigSecond);
        Assert.Throws<ArgumentOutOfRangeException>(setTooSmallSecond);
    }
    [Fact]
    public void EveryMonth_AtInvalidDay_ShouldThrowArgumentOutOrRangeException()
    {
        // Arrange
        // Act
        static IRecurrent setTooBig() => Occurs.EveryMonth().Day(0);
        static IRecurrent setTooSmall() => Occurs.EveryMonth().Day(32);
        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(setTooBig);
        Assert.Throws<ArgumentOutOfRangeException>(setTooSmall);
    }
    [Fact]
    public void EveryYear_AtInvalidMonth_ShouldThrowArgumentOutOrRangeException()
    {
        // Arrange
        // Act
        static IRecurrent setTooBig() => Occurs.EveryYear().Month(0);
        static IRecurrent setTooSmall() => Occurs.EveryYear().Month(13);
        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(setTooBig);
        Assert.Throws<ArgumentOutOfRangeException>(setTooSmall);
    }
}