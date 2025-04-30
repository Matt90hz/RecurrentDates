using IncaTechnologies.Recurrence;

namespace Tests;

public class EnumeratorTests
{
    [Fact]
    public void AsEnumerable_WhenToIsLessThanFrom_ShouldThrowArgumentException()
    {
        //Arrange
        var from = new DateTime(2000, 1, 1);
        var to = new DateTime(1999, 1, 1);
        // Act
        void enumerate() => Occurs.EveryDay().AsEnumerable(from, to);
        // Assert
        Assert.Throws<ArgumentException>(enumerate);
    }
}
