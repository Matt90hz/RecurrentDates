using IncaTechnologies.Recurrence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests;
public class ExplorerTests
{
    [Fact]
    public void FindAncestor_OnSecondlyFromYearly_ShouldReturnYearly()
    {
        // Arrange
        var occurrence = Occurs.EveryYear()
            .February()
            .SecondSunday()
            .Hour(10).Minute(12).Second(52);
        // Act
        var ancestor = occurrence.GetRoot();
        // Assert
        Assert.IsType<Yearly>(ancestor);
    }
    [Fact]
    public void FindAncestor_OnMonthlyFromMonthly_ShouldReturnMonthly()
    {
        // Arrange
        var occurrence = new Monthly();
        // Act
        var ancestor = occurrence.GetRoot();
        // Assert
        Assert.IsType<Monthly>(ancestor);
    }
}
